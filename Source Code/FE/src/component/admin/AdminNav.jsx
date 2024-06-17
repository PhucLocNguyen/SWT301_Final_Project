import { motion } from 'framer-motion';
import InsertChartIcon from '@mui/icons-material/InsertChart';
import AssignmentIndIcon from '@mui/icons-material/AssignmentInd';
import { NavLink } from 'react-router-dom';

function AdminNav() {
   return (
      <>
         <div className="h-[100%] fixed w-[20%] border-r-[1px] border-solid border-[black] overflow-hidden max-h-[100%] bg-[#f9fafb]">
            <div className="mt-[2rem] px-[1rem] ">
               <div className="flex flex-col px-[1rem]">

                  <NavLink to="/dashboard" className={({ isActive }) => `items-center text-[1.3rem] py-[0.8rem] px-[1rem] block rounded-[1rem]  ${isActive ? 'bg-[#a9dcff]' : 'hover:bg-[rgba(145,158,171,0.08)]'}`} >
                     <span className='mr-[1rem]'><InsertChartIcon fontSize='medium' /></span>
                     <span>Dashboard</span>
                  </NavLink>
                  <div className='pb-[1.5rem]'></div>
                  <NavLink href="/staff" className={({ isActive }) => `items-center text-[1.3rem] py-[0.8rem] px-[1rem] block rounded-[1rem]  ${isActive ? 'bg-[#a9dcff]' : 'hover:bg-[rgba(145,158,171,0.08)]'}`}>
                     <span className='mr-[1rem]'><AssignmentIndIcon fontSize='medium' /></span>
                     <span>Staff</span>
                  </NavLink>

               </div>
            </div>
         </div>
      </>
   )
}

export default AdminNav