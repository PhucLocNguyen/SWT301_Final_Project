import { useContext, useEffect, useState } from "react";
import { multiStepContext } from "./StepContext";
import { CustomButton } from "../../home/Home";
import {motion} from "framer-motion";
import "./style.css";
import { TextField } from "@mui/material";
import { FetchApiMaterial } from "../../../api/Requirements/FetchApiMaterial";
function FirstStep({ handleCompleteStep }) {
  const { currentStep, setCurrentStep, requirementData, setRequirementData, designRuleState , animate} =
    useContext(multiStepContext);
  const [isAllowed, setAllowed] = useState(false);
  const [data, setData] = useState({
    material: requirementData.material ==0 ? null :requirementData.material,
    size: requirementData.size ==0? null :requirementData.size,
  });
  
  const [materialList, setMaterialList] = useState([]);

  useEffect(()=>{
    const materialApi = FetchApiMaterial().then((res)=>{
      setMaterialList([...res]);
    })
    
  },[])
  useEffect(() => {
    var output = true;
    Object.entries(data).forEach(([key, value]) => {
      if (value == -1) {
        output = false;
        setAllowed(false);
        return;
      }
      if(data.material==null){
        output = false;
        setAllowed(false);
        return;
      }
      if(key==="size" && !(value>=designRuleState.MinSizeJewellery && value<=designRuleState.MaxSizeJewellery))
      {
        output = false;
        setAllowed(false);
        return;
      }
    });
    if (output) {
      setAllowed(true)};
  }, [data]);
  function NextStep(){
    if(isAllowed){
      handleCompleteStep(currentStep-1);
      setCurrentStep(currentStep+1);
      setRequirementData({...requirementData,...data })
    }
  }
  const HandleChangeData = (e) => {
    const { name, value } = e.target;
    setData({
      ...data,
      [name]: value,
    });
  };
  
  return (
    <motion.div initial={{ opacity: 0, x:50 }} whileInView={{opacity:1, x:0}} className="mx-16">
      <h2 className="text-[24px] mb-1 mt-3">Matterial </h2>
      <div className="grid grid-cols-5  mb-[30px] gap-4">
      {materialList.map((item,index)=>{
          return <label key={item.name+index} htmlFor={"material-"+index} className="rounded-md border border-[#646464] cursor-pointer" >
          <div className="shadow-lg relative h-[100px]">
          <input type="radio" name="material" id={"material-"+index} defaultChecked={requirementData.material==item.materialId} value={item.materialId} className="hidden peer" onChange={HandleChangeData} />
          <span className="w-[20px] h-[20px] mb-[50px] top-1 left-1 inline-block border-[2px] border-[#e3e3e3] rounded-full relative z-10 peer-checked:bg-primary checkedBoxFormat peer-checked:border-[#3057d5] peer-checked:scale-110 peer-checked:bg-[#3057d5] peer-checked:before:opacity-100"></span>
          <img src="https://e7.pngegg.com/pngimages/469/594/png-clipart-two-1000g-gold-bars-gold-bar-bullion-gold-bar-usb-flash-drive-gold.png" className="rounded-md w-full absolute top-0 h-[80px]"/>
          <p className="text-center">{item.name}</p>
          </div>
      </label>
        })}
      </div>
      <h2 className="text-[24px] mb-1 mt-3">Size</h2>
      <div className="relative mb-5">
      <TextField type="number" name="size" id="" value={data.size} helperText={ (typeof(data.size)==='string' && !(data.size>=designRuleState.MinSizeJewellery && data.size<=designRuleState.MaxSizeJewellery )) ?`The size must in range of ${designRuleState.MinSizeJewellery} to ${designRuleState.MaxSizeJewellery}`: null} error={(typeof(data.size)==='string' && !(data.size>=designRuleState.MinSizeJewellery && data.size<=designRuleState.MaxSizeJewellery ) )} onChange={HandleChangeData} placeholder={`Enter your size from ${designRuleState.MinSizeJewellery} to ${designRuleState.MaxSizeJewellery}.`} className="w-full" />
      <span className="checkedBoxFormat absolute"></span>
      </div>
      
      <CustomButton
        variant="contained"
        disabled={!isAllowed}
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
        onClick={NextStep}
      >
        Next
      </CustomButton>
    </motion.div>
  );
}

export default FirstStep;
