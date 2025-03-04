import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import agent from "../API/Agent";
import { useLocation } from "react-router";
import { useAccount } from "./useAccount";
import { Activity } from "../types";

export const useActivities = (id?: string) => {
    const queryClient = useQueryClient();
    const { currentUser } = useAccount();
    const location = useLocation();

    const { data: activities, isLoading } = useQuery({
        queryKey: ['activities'],
        queryFn: async () => {
            const response = await agent.get<Activity[]>('/activities');
            return response.data;
        },
        enabled: !id && location.pathname === '/activities' && !!currentUser,
        select: data => {
            return data.map(activity => {
                return {
                    ...activity,
                    isHost: currentUser?.id === activity.hostId,
                    isGoing: activity.attendees.some(x => x.id === currentUser?.id)
                }
            })
        }
    });

    const { data: activity, isLoading: isLoadingActivity } = useQuery({
        queryKey: ['activities', id],
        queryFn: async () => {
            const response = await agent.get<Activity>(`/activities/${id}`);
            return response.data;
        },
        enabled: !!id && !!currentUser,
        select: data => {
            return {
                ...data,
                isHost: currentUser?.id === data.hostId,
                isGoing: data.attendees.some(x => x.id === currentUser?.id)
            }
        }
    })

    const updateActivity = useMutation({
        mutationFn: async (activity: Activity) => {
            await agent.put('/activities', activity)
        },
        onSuccess: async () => {
            await queryClient.refetchQueries({
                queryKey: ['activities']
            })
        }
    })

    const createActivity = useMutation({
        mutationFn: async (activity: Activity) => {
            const response = await agent.post('/activities', activity);
            return response.data;
        },
        onSuccess: async () => {
            await queryClient.invalidateQueries({
                queryKey: ['activities']
            })
        }
    });

    const deleteActivity = useMutation({
        mutationFn: async (id: string) => {
            await agent.delete(`/activities/${id}`)
        },
        onSuccess: async () => {
            await queryClient.invalidateQueries({
                queryKey: ['activities']
            })
        }
    });

    const updateAttendance = useMutation({
        // mutationFn defines the function to be called when the mutation is triggered.
        mutationFn: async (id: string) => {
            // Send a POST request to mark the user as attending the activity.
            await agent.post(`/activities/${id}/attend`);
        },
        
        // onMutate is called before the mutation is fired, and is used for optimistic updates.
        onMutate: async (activityId: string) => {
            // Cancel any ongoing queries related to the specific activity being updated.
            await queryClient.cancelQueries({ queryKey: ['activities', activityId] });
    
            // Get the previous data for the activity from the query cache, to restore if needed.
            const prevActivity = queryClient.getQueryData<Activity>(['activities', activityId]);
    
            // Optimistically update the activity's data in the query cache.
            queryClient.setQueryData<Activity>(['activities', activityId], oldActivity => {
                // If no previous activity or currentUser, return the old data.
                if (!oldActivity || !currentUser) {
                    return oldActivity;
                }
    
                // Determine if the current user is the host and if they are attending.
                const isHost = oldActivity.hostId === currentUser.id;
                const isAttending = oldActivity.attendees.some(x => x.id === currentUser.id);
    
                // Return a new activity object with updated attendee list and cancellation status.
                return {
                    ...oldActivity,
                    // If the current user is the host, toggle the cancellation status.
                    isCancelled: isHost ? !oldActivity.isCancelled : oldActivity.isCancelled,
                    
                    // Update the attendees list based on whether the user is attending or not.
                    attendees: isAttending
                        ? isHost // If the user is the host, retain the attendees list unchanged.
                            ? oldActivity.attendees
                            // If the user is not the host, remove them from the attendees list.
                            : oldActivity.attendees.filter(x => x.id !== currentUser.id)
                        : [
                            // If the user is not attending, add them to the attendees list.
                            ...oldActivity.attendees,
                            {
                                id: currentUser.id,
                                displayName: currentUser.displayName,
                                imageUrl: currentUser.imageUrl
                            }
                        ]
                };
            });
    
            // Return the previous activity data in case we need to revert it if something goes wrong.
            return { prevActivity };
        },
    
        // onError is triggered if the mutation fails. We can restore the previous data here.
        onError: (error, activityId, context) => {
            // Log the error and the previous activity state to the console.
            console.log('prevActivity', context?.prevActivity);
            console.log(error);
    
            // If we have the previous activity data (context.prevActivity), restore it.
            if (context?.prevActivity) {
                queryClient.setQueryData(['activities', activityId], context.prevActivity);
            }
        }
    });
    

    return {
        activities,
        isLoading,
        updateActivity,
        createActivity,
        deleteActivity,
        activity,
        isLoadingActivity,
        updateAttendance
    }

}