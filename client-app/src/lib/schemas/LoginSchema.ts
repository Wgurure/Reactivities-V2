import { z } from "zod";

export const loginSchema = z.object({

    // Defining and validating the data from the login form
    email: z.string().email(),
    password: z.string().min(6)

})

export type LoginSchema = z.infer<typeof loginSchema>