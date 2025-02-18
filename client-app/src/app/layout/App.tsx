import { Box, Container, CssBaseline, Typography } from "@mui/material";

import { useState } from "react";
import NavBar from "./NavBar";
import ActivitesDashboard from "../../features/activities/dashboard/ActivitesDashboard";
import { useActivities } from "../../lib/hooks/useActivites";





function App() {
  //const [activities, setActivities] = useState<Activity[]>([]);
  const [selectedActivity, setSelectedActivity] = useState<Activity | undefined>(undefined); // Prop drilling 
  const [editMode, setEditMode] = useState(false);
  const {activities, isPending} = useActivities();
  

  // useEffect(() => { // triggers a side feect that gets the activites from the server side and saving it locally
  //   axios.get<Activity[]>("https://localhost:5001/api/activities").then(response  => setActivities(response.data));


  //     return () => {};
  // }, []);

  const handleSelectActivity = (id: string) => {setSelectedActivity(activities!.find(x=> x.id === id))};
  const handleCancelSelectActivity = () => {setSelectedActivity(undefined)};
  const handleOpenForm = (id?: string) => {
    if (id) {
      handleSelectActivity(id);
    } else {
      handleCancelSelectActivity();
    }
    setEditMode(true);
  };
  const handleCloseForm = () => setEditMode(false);

  // const handleSubmitForm = (activity: Activity) => {
  //   // if (activity.id) {
  //   //   setActivities(activities!.map(x => x.id === activity.id ? activity : x));
  //   // } else {
  //   //   const newActivity ={...activity, id: activities!.length.toString()}
  //   //   setSelectedActivity(newActivity)
  //   //   setActivities([...activities!, newActivity /*{ ...activity, id: activities.length.toString()*/ ]);

  //   // }
  //   console.log(activity)
  //   setEditMode(false);
  // }

  // const handleDelete = (id: string ) => {
  //   // setActivities(activities!.filter(x => x.id !== id))
  //   console.log(id)
  // }
  return (
    <Box sx={{bgcolor:'#eeeeee'}} minHeight='100vh'>
      <CssBaseline/>
      <NavBar openForm={handleOpenForm}/>
      <Container maxWidth='xl' sx={{mt : 3}}/>
        {!activities || isPending ? (<Typography>Loading.......</Typography>) : (
       <ActivitesDashboard activities = {activities!}  
       selectActivity={handleSelectActivity} 
       cancelSelectActivity={handleCancelSelectActivity}
       selectedActivity={selectedActivity}
       editMode={editMode}
       openForm={handleOpenForm}
       closeForm={handleCloseForm}
       //submitForm={handleSubmitForm}
       //deleteActivity={handleDelete} 
       /> 
       )}
      <Container/>

    </Box> 
   
  )
}

export default App
