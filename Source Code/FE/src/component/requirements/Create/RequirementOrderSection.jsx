import FirstStep from "./FirstStep";
import { multiStepContext, StepContext } from "./StepContext";
import { motion, useAnimate } from "framer-motion";
import Box from "@mui/material/Box";
import Stepper from "@mui/material/Stepper";
import Step from "@mui/material/Step";
import StepLabel from "@mui/material/StepLabel";
import { useContext, useEffect, useRef, useState } from "react";
import CurrentRequirement from "./CurrentRequirement";
import { useParams } from "react-router-dom";
import Lottie from "lottie-react"
import "./style.css";
import { useNavigate } from 'react-router-dom';
import iconSuccess from "../../../assets/icon/iconSuccess.json"
import { CustomButton } from "../../home/Home";
function RequirementOrderSection() {
  const { id } = useParams();
  const [scope, animate] = useAnimate();
const navigate = useNavigate();
  const steps = [
    "Choose option to jewellry",
    "Choose stones and Master gemstone",
    "Send your requirement",
  ];
  function moveNext(){
    navigate("/");
  }
  const [isCompleted,setIsCompleted]= useState(false);

  return (
    <>
      <div  ref={scope} className="bg-[#c9d6ff] w-full h-full py-10 bg-gradient-to-r from-purple-500 to-pink-500 flex items-center justify-center flex-col">
        {!isCompleted?<div id="boxRequirement" className="bg-[#fff]  shadow-[0_5px_15px_rgba(0,0,0,0.35)] relative z-10  w-[768px] max-w-[100%] min-h-[480px] h-max pb-10">
          <h2 className="text-center text-[32px] py-[10px]">
            Your requirement
          </h2>
          
          <StepContext designId={id} animate={animate} scope={scope} > 
            <CurrentRequirement steps={steps} animate={animate} scope={scope} setIsCompleted ={setIsCompleted}/>
          </StepContext>
        <motion.div  transition={{
                        duration: 0.6,
                        ease: 'linear'
                    }}
                    initial={{ opacity:0, zIndex:-1}} className="right-0 absolute border border-l-black top-0 bg-white h-full w-[300px] rounded-r-[30px] rounded-br-[30px]" id="MasterGemstoneContainerFloat">

                     
        </motion.div>
        </div>:<div className="bg-[#fff]  px-10 shadow-[0_5px_15px_rgba(0,0,0,0.35)] relative z-10  w-[768px] max-w-[100%] min-h-[480px] h-max pb-10">
            <div className="">
             <div className="iconNotification"><Lottie animationData={iconSuccess} loop={false}/>
            </div> 
            <h3 className="text-[32px] text-center ">Congratulations,</h3>
            <h3 className="text-[32px] text-center mb-12">Your request has been sent successfully</h3>
              <CustomButton
        variant="contained"
        onClick={moveNext}
        sx={{
          color: "#fff",
          bgcolor: "#000",
          letterSpacing: 4,
          padding: "0.7rem 2.375rem",
          fontSize: "1rem",
          fontWeight: 400,
          lineHeight: "1.5rem",
          display:"flex",
          justifyContent:"justify-center",
          width:"100%"
        }}
      >
        Go to Home Page
      </CustomButton>
            </div>
        </div>}
        
      </div>
    </>
  );
}

export default RequirementOrderSection;
