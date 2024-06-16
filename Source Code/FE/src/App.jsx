import { Routes, Route } from 'react-router-dom'
import { lazy, Suspense } from 'react'
import AuthProvider from './context/AuthContext.jsx'
import './App.css'
import Blog from "./component/blog/Blog.jsx"
import Footer from "./component/footer/Footer.jsx"
import Navbar from "./component/nav/Navbar.jsx"
import RequirementOrderSection from './component/requirements/Create/RequirementOrderSection.jsx'
import PlanningList from './component/designProduct_plan/PlanningList.jsx'

import AdminLayout from './component/admin/AdminLayout.jsx'
import StaffList from './component/admin/staffList/StaffList.jsx'
import StaffPopup from './component/admin/staffList/StaffPopup.jsx'
import Login from './component/login/Login.jsx'

import { publicRoutes } from './routes/Route.jsx'
import DefaultLayout from './component/layout/DefaultLayout.jsx'

function App() {
  return (

    <AuthProvider>
      <Suspense>
        <Routes>
          {/* Route tự viết để test */}
          <Route path='/a' element={<AdminLayout> <StaffList /> </AdminLayout>}></Route>


           <Route path='/design/1/create-requirement' element={<RequirementOrderSection />}></Route>
           <Route path='/login' element={<Login />} />

           {/* Route tự viết không ghi qua phần này */}
         
          {publicRoutes.map((route, index) => {
            let Page = route.component

            return (
              // Route cho nhung thanh phan cha
              <Route key={index} index={route.index ? true : undefined} path={route.index ? undefined : route.path} element={<DefaultLayout> <Page />  </DefaultLayout>}>

                {/* Route neu co child trong file Route.jsx */}
                {route.children && route.children.map((childRoute, childIndex) => {
                  let ChildPage = childRoute.component
                  return (
                    <Route key={childIndex} index={childRoute.index ? true : undefined} path={childRoute.index ? undefined : childRoute.path} element={<ChildPage />} />
                  )
                })}

              </Route>
            )
          })}

        </Routes>
      </Suspense>
    </AuthProvider>
  )
}

export default App
