import { lazy } from "react"
// import Home from "../component/home/Home"
// import Navbar from "../component/nav/Navbar"
// import Category from "../component/category/Category"
const Home = lazy(() => import('../component/home/Home'))
const Navbar = lazy(() => import('../component/nav/Navbar'))
const Design = lazy(() => import('../component/category/Category'))
import ListAll from "../component/category/ListAll"


const publicRoutes = [
   {
      index: true,
      component: Home
   },
   {
      path: '/design',
      component: Design,
      children: [
         { index: true, component: ListAll },
         { path: 'earring', component: ListAll },
         { path: 'bracelet', component: ListAll },
         { path: 'necklace', component: ListAll },
         { path: 'ring', component: ListAll },
      ]
   }
]

const privateRoutes = [

]

export { publicRoutes, privateRoutes }