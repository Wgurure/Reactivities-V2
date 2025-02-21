import { Box, Button, Paper, TextField, Typography } from "@mui/material";
import { useActivities } from "../../../lib/hooks/useActivites";
import { useParams } from "react-router";
import {  useForm } from 'react-hook-form';
import { useEffect } from "react";
import { ActivitySchema, activitySchema } from "../../../lib/schemas/activitySchema";
import { zodResolver } from '@hookform/resolvers/zod'

export default function ActivityForm() {
    const { register, reset, handleSubmit, formState: { errors } } = useForm<ActivitySchema>({
        mode : 'onTouched',
        resolver: zodResolver(activitySchema)
    })
    const { id } = useParams();
    const { updateActivity, createActivity, activity, isLoadingActivity } = useActivities(id)


    useEffect(() => {
        if (activity) reset(activity)
    }, [activity, reset])

    const onSubmit = (data: ActivitySchema) => {
        console.log(data)



    }

    console.log(errors); // Log the errors to see if they are captured


    if (isLoadingActivity) return <Typography>Loading Activity....... </Typography>

    return (
        <Paper sx={{ borderRadius: 3, padding: 3 }}>
            <Typography variant='h5' gutterBottom color="primary">
                {activity ? 'Edit Activity' : 'Create Activity'}
            </Typography>
            <Box component='form' onSubmit={handleSubmit(onSubmit)} display='flex' flexDirection='column' gap={3}>
                <TextField {...register('title')} label='Title' defaultValue={activity?.title} error={!!errors.title} helperText={errors.title?.message} />
                <TextField {...register('description')} label='Description' defaultValue={activity?.description} multiline rows={3} />
                <TextField {...register('category')} label='Category' defaultValue={activity?.category} />
                <TextField {...register('date')} label='Date' type='date'
                    defaultValue={activity?.date ? new Date(activity.date).toISOString().split('T')[0] : new Date().toISOString().split('T')[0]} />
                <TextField {...register('city')} label='City' defaultValue={activity?.city} />
                <TextField {...register('venue')} label='Venue' defaultValue={activity?.venue} />

                <Box display='flex' justifyContent='end' gap={3}>
                    <Button color='inherit'>Cancel</Button>
                    <Button type='submit' color='success' variant='contained' disabled={updateActivity.isPending || createActivity.isPending}>Submit</Button>
                </Box>

            </Box>

        </Paper>
    )
}