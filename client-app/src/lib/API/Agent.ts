import axios from "axios"

const sleep = (delay: number) => {new Promise(resolve =>{setTimeout(resolve, delay)})}

const agent = axios.create({
    baseURL : import.meta.env.VITE_API_URL
})

agent.interceptors.response.use(async response => {
    try {
        await sleep(5000)
        return response
    } catch (error) {
        console.log(error)
        return Promise.reject(error)
    }
})

export default agent;