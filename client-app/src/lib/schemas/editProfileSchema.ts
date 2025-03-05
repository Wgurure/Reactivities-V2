import { z } from "zod";
import { requiredString } from "../util/util";

export const editProfileSchema = z.object({

    // Defining and validating the data from the login form
    displayName : requiredString('Display Name '),
    bio : z.string().optional()

})

export type EditProfileSchema = z.infer<typeof editProfileSchema>