import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query"

import { useMemo } from "react";
import { Photo, Profile, User } from "../types";
import agent from "../API/Agent";
import { EditProfileSchema } from "../schemas/editProfileSchema";

export const useProfile = (id?: string) => {
    const queryClient = useQueryClient();

    //Fetch the user Profile 
    const { data: profile, isLoading: loadingProfile } = useQuery<Profile>({
        queryKey: ['profile', id],
        queryFn: async () => {
            const response = await agent.get<Profile>(`/profiles/${id}`); // Sends a request to the api to get the user profile 
            return response.data
        },
        enabled: !!id
    })

    // Gets all the images associated with the user profile
    const {data: photos, isLoading: loadingPhotos} = useQuery<Photo[]>({
        queryKey: ['photos', id],
        queryFn: async () => {
            const response = await agent.get<Photo[]>(`/profiles/${id}/photos`);
            return response.data;
        },

        // Casting the Id as a boolean to enable the query
        enabled: !!id
    });

    const uploadPhoto = useMutation({
        mutationFn: async (file: Blob) => {
            const formData = new FormData();
            formData.append('file', file);
            const response = await agent.post('/profiles/add-photo', formData, { // Adding the new photo the user profile photo array
                headers: {'Content-Type': 'multipart/form-data'}
            });
            return response.data;
        },
        onSuccess: async (photo: Photo) => {
            await queryClient.invalidateQueries({ // invalidates the old array of photos and replaces it with the new array
                queryKey: ['photos', id]
            });
            queryClient.setQueryData(['user'], (data: User) => {
                if (!data) return data;
                return {
                    ...data,
                    imageUrl: data.imageUrl ?? photo.url
                }
            });
            queryClient.setQueryData(['profile', id], (data: Profile) => {
                if (!data) return data;
                return {
                    ...data,
                    imageUrl: data.imageUrl ?? photo.url // if the user doesnt have a profile image, set the new image as the profile image
                }
            });
        }
    })

    const setMainPhoto = useMutation({
        mutationFn: async (photo: Photo) => {
            await agent.put(`/profiles/${photo.id}/setMain`)
        },
        onSuccess: (_, photo) => {
            queryClient.setQueryData(['user'], (userData: User) => {
                if (!userData) return userData;
                return {
                    ...userData,
                    imageUrl: photo.url
                }
            });
            queryClient.setQueryData(['profile', id], (profile: Profile) => {
                if (!profile) return profile;
                return {
                    ...profile,
                    imageUrl: photo.url
                }
            })
        }
    })

    const deletePhoto = useMutation({
        mutationFn: async (photoId: string) => {
            await agent.delete(`/profiles/${photoId}/photos`)
        },
        onSuccess: (_, photoId) => {
            queryClient.setQueryData(['photos', id], (photos: Photo[]) => {
                return photos?.filter(x => x.id !== photoId)
            })
        }
    })

    const updateProfile = useMutation({
        mutationFn: async (profile: EditProfileSchema) => {
            await agent.put('/profiles', profile )
        },
        onSuccess: (_, profile) => {
            queryClient.setQueryData(['profile', id], (data: Profile) => {
                if (!data) return data;
                return {
                    ...data,
                    displayName: profile.displayName,
                    bio: profile.bio
                }
            });
            queryClient.setQueryData(['user'], (userData: User) => {
                if (!userData) return userData;
                return {
                    ...userData,
                    displayName: profile.displayName
                }
            });
        }
    })

    // UseMemo => memorises the value of the isCurrentUser variable to avoid unnecessary API calls
    const isCurrentUser = useMemo(() => {

        // Check if the user is the current user
        return id === queryClient.getQueryData<User>(['user'])?.id
    }, [id, queryClient])

    return {
        profile,
        loadingProfile,
        photos,
        loadingPhotos,
        isCurrentUser,
        uploadPhoto,
        setMainPhoto,
        deletePhoto,
        updateProfile
    }
}