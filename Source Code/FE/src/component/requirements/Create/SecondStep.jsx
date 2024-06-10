import { motion } from "framer-motion";
import { useEffect } from "react";
import { useCallback, useContext, useState } from "react";
import { FetchApiMasterGemstone } from "../../../api/Requirements/FetchApiMasterGemstone";
import { FetchApiStones } from "../../../api/Requirements/FetchApiStones";
import { CustomButton } from "../../home/Home";
import { multiStepContext } from "./StepContext";
function SecondStep({ handleCompleteStep, completedSteps }) {
  const { currentStep, setCurrentStep, requirementData, setRequirementData } =
    useContext(multiStepContext);
    const [isAllowed, setAllowed] = useState(false);
    const [dataApiMasterGemStone, setDataApiMasterGemStone] = useState([]);
    const [filterMasterGemStone, setFilterMasterGemStone] = useState([]);
const [dataSelected, setDataSelected] = useState({
  MasterGemstone:{
    kind: null,
    shape:null,
    size: null
  } ,
  Stones:{
    kind:null,
    size: null,
    quantity:null
  }
});
//selection for MasterGemStone
  const [kindMasterGemstone, setKindMasterGemstone] = useState([]);
  const [sizeMasterGemstone, setSizeMasterGemstone] = useState([]);
  const [shapeMasterGemstone, setShapeMasterGemstone] = useState([]);

  const HandleChangeData = (e) => {
    const { name, value } = e.target;
    const dataObject = e.target.getAttribute('data_object');
    if(dataObject){
      setDataSelected((prevData) => ({
        ...prevData,
        [dataObject]: {
          ...prevData[dataObject],
          [name]: value,
        },
      }));

      dataApiMasterGemStone.filter((current)=>{
        return current=== dataSelected.MasterGemstone;
      });
    }
    };
    console.log(dataSelected)
    console.log(filterMasterGemStone);
    //initial api value when reload
    useEffect(()=>{
      const dataMaster = FetchApiMasterGemstone().then((res)=>{
        setDataApiMasterGemStone(res);
        const selectKind = new Set(res.map(item => item.kind));
        setKindMasterGemstone([...selectKind]);
        const selectSize = new Set(res.map(item => item.size));
        setSizeMasterGemstone([...selectSize]);
        const selectShape = new Set(res.map(item=> item.shape));
        setShapeMasterGemstone([...selectShape]);
      })
     
    },[]);

    // filter the selection list when choose an option to filter
useEffect(()=>{

  const selectSize = new Set(filterMasterGemStone.map(item => item.size));
        setSizeMasterGemstone([...selectSize]);
        const selectShape = new Set(filterMasterGemStone.map(item=> item.shape));
        setShapeMasterGemstone([...selectShape]);
},[filterMasterGemStone])

    console.log(dataApiMasterGemStone);
  const debounce = (func, delay) => {
    let timeoutId;
    return (...args) => {
      if (timeoutId) {
        clearTimeout(timeoutId);
      }
      timeoutId = setTimeout(() => {
        func(...args);
      }, delay);
    };
  };
  const debouncedOnChange = useCallback(debounce(HandleChangeData, 1000), [
    HandleChangeData,
  ]);
  function NextStep(){
    if(isAllowed){
      handleCompleteStep(currentStep-1);
      setCurrentStep(currentStep+1);
    }
  }
  function ToogleStone(e) {
    var key = e.target.name;
    var isChecked = e.target.checked;
    var getSection = document.getElementById(key);
    if (isChecked) {
      if (key === "MasterGemstone")
        setData({
          ...data,
          masterGemstoneId: 0,
        });
      if (key === "Stones")
        setData({
          ...data,
          stoneId: 0,
        });
        getSection.style.display="block";
    } else {
      setData({ ...data, [key]: null });
      getSection.style.display="none";
    }
  }
  return (
    <>
      <motion.div
        initial={{ opacity: 0, x: 50 }}
        whileInView={{ opacity: 1, x: 0 }}
        className="mx-16"
      >
        <div className="mb-5">
          <div className="flex justify-between content-center">
            <h2 className="text-[24px] mb-1 mt-3">Select MasterGemstone</h2>
            <input
              type="checkbox"
              className="peer"
              name="MasterGemstone"
              onClick={ToogleStone}
            />
          </div>
          <div>
            <div id="MasterGemstone">
              <div className="mb-3">
                <h4 className="text-lg">Material</h4>
                
                <div className="grid grid-cols-5 gap-x-4 mb-[30px]">
    {kindMasterGemstone.map((val, index) => {
        return (
            <label 
                key={val + index} 
                htmlFor={"material-" + index} 
                className="rounded-md border border-[#646464] cursor-pointer"
            >
                <div className="shadow-lg relative h-[100px]">
                    <input 
                        type="radio" 
                        name="kind" 
                        id={"material-" + index} 
                        value={val} 
                        className="hidden peer" 
                        data_object="MasterGemstone" 
                        onChange={debouncedOnChange} 
                    />
                    <span className="w-[20px] h-[20px] mb-[50px] top-1 left-1 inline-block border-[2px] border-[#e3e3e3] rounded-full relative z-10 peer-checked:bg-primary checkedBoxFormat peer-checked:border-[#3057d5] peer-checked:scale-110 peer-checked:bg-[#3057d5] peer-checked:before:opacity-100"></span>
                    <img 
                        src="https://e7.pngegg.com/pngimages/469/594/png-clipart-two-1000g-gold-bars-gold-bar-bullion-gold-bar-usb-flash-drive-gold.png" 
                        className="rounded-md w-full absolute top-0 h-[80px]"
                    />
                    <p className="text-center">{val}</p>
                </div>
            </label>
        );
    })}
</div>

             </div>
             <div className="grid grid-cols-2 gap-x-10">
              <div>
                <label htmlFor="size" className="text-lg">Size of Mastergemstone</label>
                <select 
                    label="Size of Mastergemstone"
                    data_object="MasterGemstone"
                    name="size"
                    key="mastergemstonesize"
                    onChange={HandleChangeData} 
                    className="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500"
                >
                    <option key="defaultSelect" value="">--Choose MasterGemStone size--</option>
                    {sizeMasterGemstone.map((items, index) => (
                        <option key={items + index} value={items}>{items}</option>
                    ))}
                </select>

              </div>
              <div>
              <label htmlFor="shape" className="text-lg">Shape of MasterGemstone</label>
              <select 
                    key="mastergemstoneshape"
                    data_object="MasterGemstone"
                    name="shape"
                    onChange={HandleChangeData}
                    className="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500"
                >
                    <option key="defaultSelectShape" value="">--Choose MasterGemStone shape--</option>
                    {
                        shapeMasterGemstone.map((item, index) => (
                            <option key={item} value={item}>{item}</option>
                        ))
                    }
               </select>
              </div>
             </div>
            </div>
          </div>
          <div className="flex justify-between content-center">
            <h2 className="text-[24px] mb-1 mt-3">Loại hạt Tấm  </h2>
            <input
              type="checkbox"
              className="peer"
              name="Stones"
              onClick={ToogleStone}
            />
          </div>
          <div id="Stones">
          <div className="grid grid-cols-2 gap-x-10">
              <div>
                <label htmlFor="size" className="text-lg">Size</label>
                <select 
                key="sizeOfStones"
                label="Size of Stones"
                data_object="Stones"
                name="size"
                onChange={HandleChangeData} 
                
                className="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500">
                  <option value="">--Choose Stones size--</option>
                  <option value="0.5">0.5</option>
                  <option value="1">1</option>
                  <option value="1.5">1.5</option>
                </select>
              </div>
              <div>
                <label htmlFor="shape" className="text-lg">Quantity of Stones</label>
                <select 
                key="quantityStones"
                  data_object="Stones"
                  name="quantity"
                  onChange={HandleChangeData}
                  className="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500" >
                  <option value="">--Choose Stones quantity--</option>
                  <option value="rectangle">16</option>
                  <option value="circle">24</option>
                  <option value="triangle">32</option>
                </select>
              </div>
             </div>
          </div>
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
    </>
  );
}

export default SecondStep;
