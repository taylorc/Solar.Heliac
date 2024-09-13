// https://nuxt.com/docs/api/configuration/nuxt-config
import * as defaults from "./src/aspnetcore-nuxt"
export default defineNuxtConfig({
  compatibilityDate: '2024-04-03',
  ssr: false,
  devtools: { enabled: true },
  devServer: {
    https: {
        cert: defaults.certFilePath,
        key: defaults.keyFilePath
    }
},
css: ['~/assets/css/main.css'],
postcss: {
  plugins: {
    tailwindcss: {},
    autoprefixer: {},
  },
},
plugins: ["~/plugins/preline.client.ts"],  
})
