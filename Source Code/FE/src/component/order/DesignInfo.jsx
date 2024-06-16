import { useEffect, useState } from 'react';
import { useParams,useLocation } from 'react-router-dom';
import Button from '@mui/material/Button'
import { styled } from '@mui/material/styles';
import { Link } from 'react-router-dom';
import { fetchApiDesignById } from '../../api/FetchApiDesign';
import CheckCircleOutlineIcon from '@mui/icons-material/CheckCircleOutline';
import DesignImage from '../../assets/designInfo/jewelry.png'
import Arrow from '../../assets/designInfo/arrow.svg'
import Design1 from '../../assets/designInfo/design1.png'

const CustomButton = styled(Button)({
   '&:hover': {
      backgroundColor: '#fff',
      color: '#000',
      border: '1px solid #000'
   }
})

function DesignInfo() {
   const [designInfo, setDesignInfo] = useState({});
   const { id } = useParams()

   useEffect(() => {
      const fetchAPI = async () => {
         const respone = await fetchApiDesignById(id)
         setDesignInfo(respone)
      }

      fetchAPI()
   }, [])

   console.log(designInfo)

   return (
      <>
         <div className="py-[8.125rem]">
            <div className="max-w-[75rem] ml-auto mr-auto flex items-center flex-col">
               <div className="flex w-[100%] gap-x-[3.5rem]">
                  <div className='max-w-[40%]  rounded-lg'>
                     {/* Hinh anh */}
                     <img src={designInfo.image} className='max-w-[100%] w-[30rem] max-h-[30rem] h-[30rem] object-cover rounded-lg' />
                  </div>
                  <div className='flex flex-col items-start gap-y-[1.5rem] max-w-[55%]'>
                     {/* Ten thiet ke */}
                     <h1 className='text-[#000] mt-[0.7rem] text-[4rem] font-normal leading-[4.5rem] '>
                        {designInfo.designName}
                     </h1>
                     {/* Description Section */}
                     <div className='flex flex-col gap-y-[0.5rem] min-h-[9.3rem]'>
                        <h3 className='text-[2rem] font-normal leading-[2.875rem]'>Description</h3>
                        {/* Phan mo ta thiet ke */}
                        <p className='text-[#000] mb-0 text-[1rem] font-normal leading-[1.5rem]'>
                           Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap
                        </p>
                     </div>

                     {/* Policy section */}
                     <div className='gap-y-[10px] flex flex-col'>
                        <div className='flex items-center'>
                           <CheckCircleOutlineIcon color='primary' />
                           <span className='ml-[5px] text-[#000] text-[1rem] font-normal leading-[1.5rem]'>Product prices vary depending on actual weight of ingredients.</span>
                        </div>
                        <div className='flex items-center'>
                           <CheckCircleOutlineIcon color='primary' />
                           <span className='ml-[5px] text-[#000] text-[1rem] font-normal leading-[1.5rem]'>Customers can customize the design easily and quickly.</span>
                        </div>
                        <div className='flex items-center'>
                           <CheckCircleOutlineIcon color='primary' />
                           <span className='ml-[5px] text-[#000] text-[1rem] font-normal leading-[1.5rem]'>Warranty for orders is at least 1 year.</span>
                        </div>
                     </div>

                     {/* Button custom */}
                     <Link to={`/design/create-requirement/${id}`} style={{ width: '100%' }}>
                        <CustomButton variant='contained' sx={{ color: '#fff', bgcolor: '#000', letterSpacing: 4, padding: '0.7rem 2.375rem', fontSiz: '1rem', fontWeight: 400, lineHeight: '1.5rem', width: '100%' }} >
                           CUSTOM DESIGN
                        </CustomButton>
                     </Link>

                  </div>
               </div>
            </div>
         </div>

         {/* Related design */}
         <div className='pb-[8.125rem]'>
            <div className="max-w-[75rem] ml-auto mr-auto flex items-center flex-col">
               {/* Start */}
               <div className='w-[100%]'>
                  <div className='w-[100%] flex mb-[3rem] items-center justify-between'>
                     <h2 className='text-[2.625rem] font-normal leading-[3.5rem]'>RELATED DESIGN</h2>
                     <a className='tracking-[4px] flex items-center justify-start font-normal '>
                        <div>VIEW MORE</div>
                        <img className='translate-x-[-1.4rem]' src={Arrow} />
                     </a>
                  </div>

                  {/* Related design Item  */}
                  <div>
                     <div className='grid gap-x-[1.5rem] grid-rows-1 grid-cols-3'>
                        {/* ---------- */}
                        <div>
                           <a className='relative overflow-hidden max-w-[100%] inline-block '>
                              <img className='inline-block max-w-[100%] overflow-hidden' src={Design1} />
                              <div className='absolute top-[24px] left-[30px] font-light tracking-[1px] text-[1rem] leading-[1.5rem]'>Description of the design</div>
                           </a>
                        </div>
                        <div>
                           <a className='relative overflow-hidden max-w-[100%] inline-block '>
                              <img className='inline-block max-w-[100%] overflow-hidden' src={Design1} />
                              <div className='absolute top-[24px] left-[30px] font-light tracking-[1px] text-[1rem] leading-[1.5rem]'>Description of the design</div>
                           </a>
                        </div>
                        <div>
                           <a className='relative overflow-hidden max-w-[100%] inline-block '>
                              <img className='inline-block max-w-[100%] overflow-hidden' src={Design1} />
                              <div className='absolute top-[24px] left-[30px] font-light tracking-[1px] text-[1rem] leading-[1.5rem]'>Description of the design</div>
                           </a>
                        </div>
                        {/* ---------- */}
                     </div>
                  </div>
                  {/* ------------------------------ */}

               </div>
            </div>
         </div>
      </>
   )
}

export default DesignInfo