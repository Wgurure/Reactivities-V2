import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react-swc'


// https://vite.dev/config/
export default defineConfig({
  server: { 
    port: 3000,
    https: {
      key: "C:/Users/WGurure/localhost+2-key.crt",
      cert: "C:/Users/WGurure/localhost+2.crt",
    },
    hmr: {
      protocol: 'wss',
      host: 'localhost',
    },
  },
  plugins: [react()],

})
