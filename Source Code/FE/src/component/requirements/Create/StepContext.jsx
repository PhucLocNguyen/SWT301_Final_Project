import { useState } from "react";
import { createContext } from "react";

export const multiStepContext = createContext();
export function StepContext({children}) {
    const [currentStep, setCurrentStep] = useState(1);
    const [requirementData, setRequirementData] = useState({
        designParentId: 1,
        material: 0,
        size: 0,
        stoneId: 0,
        masterGemstoneId:0,
        stonesId: 0,
        customerNote:"",
    });
    const [finalData, setFinalData] = useState([]);
    console.log(requirementData);
    return (  <>
    <multiStepContext.Provider value={{currentStep, setCurrentStep, requirementData, setRequirementData, finalData, setFinalData}}>
        {children}
    </multiStepContext.Provider>
    </>);
}