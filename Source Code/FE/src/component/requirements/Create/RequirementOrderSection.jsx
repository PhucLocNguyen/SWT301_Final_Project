import FirstStep from "./FirstStep";
import { multiStepContext, StepContext } from "./StepContext";
import { motion } from "framer-motion";
import Box from "@mui/material/Box";
import Stepper from "@mui/material/Stepper";
import Step from "@mui/material/Step";
import StepLabel from "@mui/material/StepLabel";
import { useContext } from "react";
import CurrentRequirement from "./CurrentRequirement";
function RequirementOrderSection() {
  const steps = [
    "Choose option to jewellry",
    "Choose stones and Master gemstone",
    "Send your requirement",
  ];

  return (
    <>
      <div className="bg-[#c9d6ff] w-full h-screen bg-gradient-to-r from-purple-500 to-pink-500 flex items-center justify-center flex-col">
        <div className="bg-[#fff] rounded-[30px] shadow-[0_5px_15px_rgba(0,0,0,0.35)] relative overflow-hidden w-[768px] max-w-[100%] min-h-[480px] h-max pb-10">
          <h2 className="text-center text-[32px] py-[10px]">
            Your requirement
          </h2>
          
          <StepContext>
            <CurrentRequirement steps={steps} />
          </StepContext>
        </div>
      </div>
    </>
  );
}

export default RequirementOrderSection;
