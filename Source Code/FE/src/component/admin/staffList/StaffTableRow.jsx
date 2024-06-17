import IconButton from '@mui/material/IconButton';
import MoreVertIcon from '@mui/icons-material/MoreVert';
import TableRow from '@mui/material/TableRow';
import TableCell from '@mui/material/TableCell';
import Popover from '@mui/material/Popover';
import MenuItem from '@mui/material/MenuItem';
import { useState } from 'react';


function StaffTableRow({ data }) {
   const [open, setOpen] = useState(null);

   const handleOpenMenu = (event) => {
      setOpen(event.currentTarget);
   }

   const handleCloseMenu = () => {
      setOpen(null);
   };

   return (
      <>
         <TableRow>
            <TableCell align='left'>{data.name}</TableCell>
            <TableCell align='left'>{data.role}</TableCell>
            <TableCell align='left'>{data.email}</TableCell>
            <TableCell align='left'>{data.phone}</TableCell>
            <TableCell>
               <IconButton onClick={handleOpenMenu}>
                  <MoreVertIcon />
               </IconButton>
            </TableCell>
         </TableRow>

         <Popover
            open={!!open}
            anchorEl={open}
            onClose={handleCloseMenu}
            anchorOrigin={{ vertical: 'top', horizontal: 'left' }}
            transformOrigin={{ vertical: 'top', horizontal: 'right' }}
            PaperProps={{
               sx: { width: 140 },
            }}
         >
            <MenuItem onClick={handleCloseMenu}>
               
               Edit
            </MenuItem>

            <MenuItem onClick={handleCloseMenu} sx={{ color: 'error.main' }}>
               
               Delete
            </MenuItem>
         </Popover>
      </>
   )
}

export default StaffTableRow