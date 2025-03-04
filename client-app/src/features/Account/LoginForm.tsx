import { useForm } from 'react-hook-form'
import { useAccount } from '../../lib/hooks/useAccount'
import { loginSchema, LoginSchema } from '../../lib/schemas/LoginSchema'
import { zodResolver } from '@hookform/resolvers/zod'
import { Box, Button, Paper, Typography } from '@mui/material'
import { LockOpen } from '@mui/icons-material'
import TextInput from '../../app/shared/components/TextInput'
import { Link, useLocation, useNavigate } from 'react-router'

export default function LoginForm() {
const {loginUser}= useAccount()

const navigate = useNavigate()
const location = useLocation()

// CONTROL: allows you to access the data that will be inputed in the form field 
//HANDLESUBMIT: provides the actions that should be performed on submit of the form
//FORMSTATE: tracks the state of the form (isValid: does it meet the validation rules (bool)) and (isSubmitting)
const {control, handleSubmit, formState: {isValid, isSubmitting}} = useForm<LoginSchema>({
    mode: 'onTouched', // Performs the validation when the login field is touched
    resolver: zodResolver(loginSchema) // validates the data according to the loginSchema


})

const onSubmit = async (data: LoginSchema) => {
    await loginUser.mutateAsync(data , {
        onSuccess: () => {
           navigate(location.state?.from || '/activities') 
        }
    })// submmiting data to the server 
}
    return (
        <Paper component='form'
            onSubmit={handleSubmit(onSubmit)}
            sx={{
            display: 'flex',
            flexDirection: 'column',
            p: 3,
            gap: 3,
            maxWidth: 'md',
            mx: 'auto',
            borderRadius: 3}}>
                <Box display='flex' alignItems='center' 
                justifyContent='center' 
                gap={3} color='secondary.main'>
                    <LockOpen fontSize="large" /> 
                    <Typography variant="h4">Sign in</Typography>
                </Box>
                <TextInput label='Email' control={control} name='email' />
                <TextInput label='Password' type='password' control={control} name='password' />
                <Button 
                type='submit' 
                disabled={!isValid || isSubmitting} // Disable the button if the form data is not valid of if the form is being submitted  
                variant="contained"
                size="large"
                >
                    Login
                </Button>
                <Typography sx={{textAlign: 'center'}}>
                    Don't have an account? 
                    <Typography sx={{ml: 1}} component={Link} to='/register' color="primary">
                        Sign up
                    </Typography>
                </Typography>
        </Paper>
    )
}
