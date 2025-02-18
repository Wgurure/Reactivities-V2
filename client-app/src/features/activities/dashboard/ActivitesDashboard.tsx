import { Grid2 } from "@mui/material";
import ActivityList from "./ActivityList";
import ActivityDetails from "../../details/ActivityDetails";
import ActivityForm from "../../details/form/ActivityForm";

type Props = {
    activities: Activity[] 
    selectActivity: (id: string) => void
    cancelSelectActivity: () => void
    selectedActivity?: Activity
    openForm: (id: string) => void 
    closeForm: () => void
    editMode: boolean
    //submitForm : (activity: Activity) => void
    //deleteActivity: (id: string) => void
}

export default function ActivitesDashboard({activities, cancelSelectActivity, selectedActivity, selectActivity, openForm, closeForm, editMode} :Props) {
  return (
    <Grid2 container>
        <Grid2 size={7}>
          <ActivityList activities={activities}
          selectActivity={selectActivity} 
          //deleteActivity={deleteActivity}
          />
        </Grid2>
        <Grid2 size={5}>
          {selectedActivity && !editMode && <ActivityDetails selectedActivity={selectedActivity} cancelSelectActivity={cancelSelectActivity} openForm={openForm} />}
          {editMode &&
          <ActivityForm closeForm={closeForm} activity={selectedActivity} />}
        </Grid2>
    </Grid2>
  )
}
