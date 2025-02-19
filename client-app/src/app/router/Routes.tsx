import { createBrowserRouter } from "react-router";
import App from "../layout/App";
import HomePage from "../../features/home/HomePage";
import ActivityForm from "../../features/details/form/ActivityForm";
import ActivitesDashboard from "../../features/activities/dashboard/ActivitesDashboard";
import ActivityDetails from "../../features/details/ActivityDetailsPage";
import Counter from "../../features/counter/Counter";

export const router = createBrowserRouter([
    {
        path: '/',
        element: <App />,
        children: [
            {path: '', element: <HomePage/>},
            {path: 'activities', element: <ActivitesDashboard />},
            {path: 'activities/:id', element: <ActivityDetails/>},
            {path: 'createActivity', element: <ActivityForm key='create'/>},
            {path: 'manage/:id', element: <ActivityForm />},
            {path: 'counter', element: <Counter />}
        ]
    }
])