import FirstStep from "./FirstStep";
import { multiStepContext, StepContext } from "./StepContext";
import { motion } from "framer-motion";
import Box from "@mui/material/Box";
import Stepper from "@mui/material/Stepper";
import Step from "@mui/material/Step";
import StepLabel from "@mui/material/StepLabel";
import { useContext, useState } from "react";
import { CustomButton } from "../../home/Home";
import SecondStep from "./SecondStep";
import ThirdStep from "./ThirdStep";
function CurrentRequirement({steps}) {
    const {currentStep, setCurrentStep} = useContext(multiStepContext);
    const [completedSteps, setCompletedSteps] = useState(
        Array(steps.length).fill(false)
      );
      const handleCompleteStep = (step) => {
        setCompletedSteps((prevCompletedSteps) =>
          prevCompletedSteps.map((completed, index) =>
            index === step ? true : completed
          )
        );
      };
    console.log(completedSteps);
      const handleStepClick = (index) => {
        if (completedSteps[index]) {
          // Nếu giai đoạn đã hoàn thành, cho phép chuyển đến giai đoạn tiếp theo
          setCurrentStep(index + 1);
        }
      };
      function ShowStep({step,setCompletedSteps}){
        switch(step){
            case 1: 
            return <FirstStep handleCompleteStep ={handleCompleteStep}/>
            case 2:
                return <SecondStep handleCompleteStep ={handleCompleteStep} completedSteps={completedSteps} />
            case 3:
                return <ThirdStep handleCompleteStep ={handleCompleteStep}/>
        }
      }
    return ( <>
    <Box sx={{ width: "100%" }}>
            <Stepper activeStep={currentStep-1} alternativeLabel>
              {steps.map((label, index) => (
                <Step key={label} className="cursor-pointer">
                  <StepLabel onClick={()=>handleStepClick(index)}>{label} </StepLabel>
                </Step>
              ))}
            </Stepper>
          </Box>
          <ShowStep step={currentStep} setCompletedSteps={setCompletedSteps} />
          </> );
}

export default CurrentRequirement;