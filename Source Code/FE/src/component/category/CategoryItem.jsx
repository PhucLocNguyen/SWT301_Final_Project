import { Link } from 'react-router-dom'
import { motion } from 'framer-motion'
import Arrow from '../../assets/categoryItem/arrow.svg'

function CategoryItem({design = {}}) {

   return (
      <>
         <div className="text-[1rem] leading-[1.3em] font-normal">
            <Link to={`/design/${design.designId}`} className="max-w-[100%] inline-block cursor-pointer">
               <div className='overflow-hidden mb-[0.94rem] rounded-[10px]'>
                  <motion.img whileHover={{ scale: 1.1 }} transition={{ duration: .7 }} className='rounded-[10px] inline-block max-w-[100%]' src={design.image} />
               </div>
               <div className='flex justify-between items-center'>
                  <div>
                     <h6 className='text-[1.5rem] font-normal '>{design.designName}</h6>
                  </div>
                  <div>
                     <div className='flex w-[3.125rem] h-[3.125rem] justify-center items-center	border-solid border-[1px] border-[#000] rounded-[50%]'>
                        <img className='max-w-[100% inline-block' src={Arrow} />
                     </div>
                  </div>
               </div>
            </Link>
         </div>
      </>
   )
}

export default CategoryItem