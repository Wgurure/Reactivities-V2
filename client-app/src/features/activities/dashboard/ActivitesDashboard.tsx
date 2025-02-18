import { Grid2} from "@mui/material";
import ActivityList from "./ActivityList";




export default function ActivitesDashboard() {
    return (
      <Grid2 container>
        <Grid2 size={7}>
          <ActivityList />
        </Grid2>
        <Grid2 size={5}>
          Activity Filters go here 
        </Grid2>
      </Grid2>
  )}

