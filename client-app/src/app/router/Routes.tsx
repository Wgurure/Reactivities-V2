import { createBrowserRouter, Navigate } from "react-router";
import App from "../layout/App";
import HomePage from "../../features/home/HomePage";
import ActivityForm from "../../features/form/ActivityForm";
import ActivitesDashboard from "../../features/activities/dashboard/ActivitesDashboard";
import ActivityDetails from "../../features/details/ActivityDetailsPage";
import Counter from "../../features/counter/Counter";
import TestErrors from "../../features/errors/TestErrrors";
import NotFound from "../../features/errors/NotFound";
import ServerError from "../../features/errors/ServerError";
import LoginForm from "../../features/Account/LoginForm";
import RequireAuth from "./RequireAuth";
import RegisterForm from "../../features/Account/RegisterForm";

export const router = createBrowserRouter([
    {// The default path for when the user has entered the page and the react element that should be rendered when that route is selected 
        path: '/',
        element: <App />,
        children: [
            {element: <RequireAuth/>, children: [
                {path: 'activities', element: <ActivitesDashboard />},
                {path: 'activities/:id', element: <ActivityDetails/>},
                {path: 'createActivity', element: <ActivityForm key='create'/>},
                {path: 'manage/:id', element: <ActivityForm />},
            ]},
            {path: '', element: <HomePage/>},
            
            {path: 'counter', element: <Counter />},
            {path: 'errors', element: <TestErrors />},
            {path: 'not-found', element: <NotFound />},
            {path: 'server-error', element: <ServerError />},
            {path: 'login', element: <LoginForm/>},
            {path: 'register', element: <RegisterForm/>},
            {path: '*', element: <Navigate replace to='/not-found' />},
        ]
    }
])