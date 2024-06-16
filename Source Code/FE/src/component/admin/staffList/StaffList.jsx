import Button from '@mui/material/Button';
import AddIcon from '@mui/icons-material/Add';
import TextField from '@mui/material/TextField';
import TableContainer from '@mui/material/TableContainer';
import TableHead from '@mui/material/TableHead';
import TableRow from '@mui/material/TableRow';
import Table from '@mui/material/Table';
import TableBody from '@mui/material/TableBody';
import TableCell from '@mui/material/TableCell';

import { useState } from 'react';

import StaffTableRow from './StaffTableRow';
import StaffPopup from './StaffPopup';

function StaffList() {

   const [isOpenPopup,setIsOpenPopup] = useState(false)

   const rows = [
      { name: 'Nguyen Duc Hung', role: 'Design staff', email: 'hungndse171325@fpt.edu.vn', phone: '01234567899' },
      { name: 'Nguyen trong THien', role: 'Design staff', email: 'hungndse171325@fpt.edu.vn', phone: '01234567899' },
      { name: 'Nguyen Gia Khanh', role: 'Design staff', email: 'hungndse171325@fpt.edu.vn', phone: '01234567899' },
      { name: 'Nguyen Phuc Loc', role: 'Design staff', email: 'hungndse171325@fpt.edu.vn', phone: '01234567899' },
      { name: 'Nguyen Ba Dat', role: 'Design staff', email: 'hungndse171325@fpt.edu.vn', phone: '01234567899' },
   ];

   return (
      <>
         <div className="py-[3.5rem] px-[2.5rem] ">
            <div className="flex items-center justify-between mb-[3rem]">
               <h4 className="text-[1.5rem] font-bold leading-[1.5]">Staff List</h4>
               <Button onClick={() => setIsOpenPopup(true)} startIcon={<AddIcon />} variant="contained">New Staff</Button>
            </div>

            <div>

               {/* Search */}
               <div className='px-[1rem] py-[1rem] bg-[#fff] rounded-t-[20px]'>
                  <TextField id="outlined-basic" placeholder='Search Staff' variant="outlined" size='small'/>
               </div>

               {/* Staff table */}
               <div>

                  <TableContainer>
                     <Table>
                        <TableHead>
                           <TableRow>
                              <TableCell align='left'>Name</TableCell>
                              <TableCell align='left'>Role</TableCell>
                              <TableCell align='left'>Email</TableCell>
                              <TableCell align='left'>Phone</TableCell>
                              <TableCell align='left'></TableCell>
                           </TableRow>
                        </TableHead>

                        <TableBody>
                           {rows.map((item, index) => {
                              return (
                                 <StaffTableRow key={index} data={item} />
                              )
                           })}
                        </TableBody>
                     </Table>
                  </TableContainer>

               </div>

            </div>

         </div>

         {isOpenPopup && <StaffPopup setIsOpenPopup={setIsOpenPopup} />}
      </>
   )
}

export default StaffList