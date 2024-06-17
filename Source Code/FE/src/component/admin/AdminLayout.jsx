import AdminNav from "./AdminNav"
import AdminHeader from "./AdminHeader"

function AdminLayout({children}) {
   return(
      <>
         <div className="flex bg-[#f9fafb]" >
            <div className="w-[20%] min-h-screen">
               <AdminNav />
            </div>
            <div className="w-[80%] ">
               <AdminHeader/>

               <div>
                  {children}
               </div>

            </div>
         </div>
      </>
   )
}

export default AdminLayout