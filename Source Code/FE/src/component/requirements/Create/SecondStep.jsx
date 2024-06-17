import { motion } from "framer-motion";
import { useEffect } from "react";
import { useContext, useState } from "react";
import { FetchApiMasterGemstone, FetchApiMasterGemstoneById } from "../../../api/Requirements/FetchApiMasterGemstone";
import { FetchApiStones, FetchApiStonesById } from "../../../api/Requirements/FetchApiStones";
import { CustomButton } from "../../home/Home";
import { multiStepContext } from "./StepContext";
import RestartAltIcon from '@mui/icons-material/RestartAlt';
import { IconButton, Tooltip } from "@mui/material";
import ShowMasterGemStone from "./ShowMasterGemStone";
function SecondStep({ handleCompleteStep, completedSteps }) {
  const { currentStep, setCurrentStep, requirementData, setRequirementData, designRuleState, animate, scope } =
    useContext(multiStepContext);
    const [isAllowed, setAllowed] = useState(false);
    const [dataApiMasterGemStone, setDataApiMasterGemStone] = useState([]);
    const [filterMasterGemStone, setFilterMasterGemStone] = useState([]);
    const [dataApiStones, setDataApiStones] = useState([]);
    const [filterStones, setFilterStones] = useState([]);
    const [isToggle, setIsToggle] = useState(false);
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
 
  //Selection for Stones
  const [kindStones, setKindStones] = useState([]);
  const [sizeStones, setSizeStones] = useState([]);
  const [quantityStones, setQuantityStones] = useState([]);

  // Result after select
  const [masterGemstoneObject, setMasterGemstoneObject]= useState({});
  const [stonesObject, setStonesObject] = useState({});
  //initial api value when reload
    useEffect(()=>{
      const dataMaster = FetchApiMasterGemstone(designRuleState.MinSizeMasterGemstone,designRuleState.MaxSizeMasterGemstone).then((res)=>{
        setDataApiMasterGemStone(res);
        setFilterMasterGemStone(res);
        const selectKind = new Set(res.map(item => item.kind));
        setKindMasterGemstone([...selectKind]);
        const selectSize = new Set(res.map(item => item.size));
        setSizeMasterGemstone([...selectSize]);
        const selectShape = new Set(res.map(item=> item.shape));
        setShapeMasterGemstone([...selectShape]);
      })
      
      const dataStones = FetchApiStones().then((res)=>{
        setDataApiStones(res);
        setFilterStones(res);
        const selectKind = new Set(res.map(item => item.kind));
        setKindMasterGemstone([...selectKind]);
        const selectSize = new Set(res.map(item => item.size));
        setSizeMasterGemstone([...selectSize]);
        const selectQuantity = new Set(res.map(item=> item.quantity));
        setShapeMasterGemstone([...selectQuantity]);
      })

      // set du lieu khi da select nhung quay lai step nay de coi chinh sua tiep
      var dataFetching = {};
      const promises = [];
  
      if (requirementData.masterGemstoneId != null && requirementData.masterGemstoneId > 0) {
        promises.push(
          FetchApiMasterGemstoneById(requirementData.masterGemstoneId).then((res) => {
            dataFetching = {
              ...dataFetching,
              MasterGemstone: {
                kind: res.kind,
                shape: res.shape,
                size: res.size
              }
            };
          })
        );
      }
  
      if (requirementData.stonesId != null && requirementData.stonesId > 0) {
        promises.push(
          FetchApiStonesById(requirementData.stonesId).then((res) => {
            dataFetching = {
              ...dataFetching,
              Stones: {
                kind: res.kind,
                size: res.size,
                quantity: res.quantity
              }
            };
          })
        );
      }
  
      Promise.all(promises).then(() => {
        if (Object.keys(dataFetching).length > 0) {
          setDataSelected(prevData => ({ ...prevData, ...dataFetching }));
        }
      });
      let objectChange = {};
      var isSelectedBefore = false;

      // toogle master gemstone & stones  visible or hidden
      if(requirementData.masterGemstoneId==null){
        objectChange = {...objectChange, MasterGemstone: null};
        var getSection = document.getElementById("MasterGemstone");
        getSection.style.display="none";
        isSelectedBefore = true;
      }
      if(requirementData.stonesId==null){
        objectChange = {...objectChange, Stones: null};

        var getSection = document.getElementById("Stones");
        getSection.style.display="none";
        isSelectedBefore = true;
      }
      if(isSelectedBefore){
        setDataSelected({...dataSelected, ...objectChange});
      }
      
    
    },[]);

    // filter the selection list when choose an option to filter
    useEffect(() => {
      var output = true;
      // tim ra id cho mastergemstones
      if(dataSelected.MasterGemstone!==null){
      const dataFilterLastMasterGemstone = dataApiMasterGemStone.filter((current) => {
        return Object.keys(dataSelected.MasterGemstone).every((key) => {
          const selectedValue = dataSelected.MasterGemstone[key];
          if (selectedValue !== null && selectedValue !== "") {
            return current[key] == selectedValue;
          }
          return true;
        })});
        if(dataFilterLastMasterGemstone.length ==1){
          setMasterGemstoneObject(dataFilterLastMasterGemstone[0]);
          ShowMasterGemStone(dataFilterLastMasterGemstone[0]);
          var target = scope.current.querySelector("#MasterGemstoneContainerFloat");
          target.style.display="block";
          setIsToggle(true);
        }
        // animation box mastergemstone
        if(dataFilterLastMasterGemstone.length == 1 && !isToggle){
          
          animate("div#boxRequirement", {x: [0,-150]});
        animate("div#boxRequirement #MasterGemstoneContainerFloat",{x:[0,"300px"], opacity:[0,1], zIndex: [-1,1]});

        }
      }else{
        setIsToggle(false);
        animate("div#boxRequirement", {x: 0});
        animate("div#boxRequirement #MasterGemstoneContainerFloat",{x:["300px",0], opacity:[1,0], zIndex: [1,-1]});
        setMasterGemstoneObject({masterGemstoneId:null});
      }
      // tim ra id cho stones
      if(dataSelected.Stones!==null){
        const dataFilterLastStones = dataApiStones.filter((current) => {
          return Object.keys(dataSelected.Stones).every((key) => {
            const selectedValue = dataSelected.Stones[key];
            if (selectedValue !== null && selectedValue !== "") {
              return current[key] == selectedValue;
            }
            return true;
          })});

          if(dataFilterLastStones.length ==1){
            
            setStonesObject(dataFilterLastStones[0])
          }
      }else{
        setStonesObject({stonesId:null});
      }

    // check dieu kien de di tiep
    if(dataSelected.MasterGemstone!==null){
        Object.entries(dataSelected.MasterGemstone).forEach(([key, value]) => {
          if(value==""|| value ==null){
            output=false;
            setAllowed(false);
            return;
          }
      });
    }
    if(dataSelected.Stones!==null){

      Object.entries(dataSelected.Stones).forEach(([key, value]) => {
        if(value==""|| value ==null){
          output=false;
          setAllowed(false);
          return;
        }
    })
    }

     if(output){
      setAllowed(true);
     }

     var target = scope.current.querySelector("#MasterGemstoneContainerFloat");
     if(dataSelected.MasterGemstone==null){
       target.style.display="none";
     }
    },[dataSelected]);
    // 2 list thay doi khi show ra FE
useEffect(()=>{
  const selectKind = new Set(filterMasterGemStone.map(item => item.kind));
        setKindMasterGemstone([...selectKind]);

  const selectSize = new Set(filterMasterGemStone.map(item => item.size));
        setSizeMasterGemstone([...selectSize]);
        const selectShape = new Set(filterMasterGemStone.map(item=> item.shape));
        setShapeMasterGemstone([...selectShape]);

},[filterMasterGemStone])

useEffect(()=>{
  const selectKind = new Set(filterStones.map(item => item.kind));
        setKindStones([...selectKind]);

  const selectSize = new Set(filterStones.map(item => item.size));
        setSizeStones([...selectSize]);
        const selectQuantity = new Set(filterStones.map(item=> item.quantity));
        setQuantityStones([...selectQuantity]);
},[filterStones])

const HandleChangeData = (e) => {
  const { name, value } = e.target;
  const dataObject = e.target.getAttribute('data_object');

  //loc list boi da chu
  if(dataObject ==="MasterGemstone"){
  var filterData = [];
    if(name === "kind"){
      filterData = dataApiMasterGemStone.filter((current)=>{
        return current[name]===value;
      });
    }else{
      if(value!==""){
      filterData = filterMasterGemStone.filter((current) => {
        return current[name] == value;
      })}else{
        filterData = dataApiMasterGemStone.filter((current)=>{
          if(dataSelected.MasterGemstone.kind!==null && dataSelected.MasterGemstone.kind!==""){
  
            return current["kind"]==dataSelected.MasterGemstone.kind;
          }
          return true;
        });
      }
    }
    setFilterMasterGemStone(filterData);
  }

  // loc list hat tam

  if(dataObject ==="Stones"){
    var filterData=[];
    if(name === "kindStones"){
      filterData = dataApiStones.filter((current)=>{
        return current["kind"]===value;
      });
    }else{
      if(value!==""){
      filterData = filterStones.filter((current) => {
        return current[name] == value;
      })}else{
        filterData = dataApiStones.filter((current)=>{
          if(dataSelected.Stones.kind!==null && dataSelected.Stones.kind!==""){
            return current["kind"]==dataSelected.Stones.kind;
          }
          return true;
        });
      }
    }
    setFilterStones(filterStones);
  } 

  //Them du lieu vao data select
  if(dataObject){
    if(dataObject=="Stones"&&name==="kindStones"){
      setDataSelected((prevData) => ({
        ...prevData,
        [dataObject]: {
          ...prevData[dataObject],
          ["kind"]: value,
        },
      }));
    }else{
      setDataSelected((prevData) => ({
        ...prevData,
        [dataObject]: {
          ...prevData[dataObject],
          [name]: value,
        },
      }));
    }
  }
  };
  const handleReset = () => {
    setDataSelected((prevData) => ({
      ...prevData,
      ["MasterGemstone"]: {
        ...prevData["MasterGemstone"],
        ["kind"]: '',
      },
    }));
    
    setFilterMasterGemStone(dataApiMasterGemStone);
};


  function NextStep(){
    if(isAllowed){
      animate("div#boxRequirement", {x: 0});
      animate("div#boxRequirement #MasterGemstoneContainerFloat",{x:["300px",0], opacity:[1,0], zIndex: [1,-1]});
      setRequirementData({...requirementData,
        masterGemstoneId: masterGemstoneObject.masterGemstoneId,
        stonesId:stonesObject.stonesId,});
      handleCompleteStep(currentStep-1);
      setCurrentStep(currentStep+1);
    }
  }


  //view or disabled
  function ToogleStone(e) {
    var key = e.target.name;
    var isChecked = e.target.checked;
    var getSection = document.getElementById(key);
    if (isChecked) {
      if (key === "MasterGemstone")
        setDataSelected({
          ...dataSelected,
          MasterGemstone:{
            kind: null,
            shape:null,
            size: null
          } ,
        });
      if (key === "Stones")
        setDataSelected({
          ...dataSelected,
          Stones:{
            kind:null,
            size: null,
            quantity:null
          },
        });
        getSection.style.display="block";
    } else {
      setDataSelected({ ...dataSelected, [key]: null });
      getSection.style.display="none";
    }
  }
  return (
    <>
      <motion.div 
      animate={{
        x: isToggle ? '100%' : 0,
        opacity: isToggle ? [0, 1] : 0,
        zIndex: isToggle ? 3 : 1
    }}
    transition={{
        duration: 0.6,
        ease: 'linear'
    }}
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
              defaultChecked={(requirementData.masterGemstoneId!==null &&requirementData.masterGemstoneId>0) || (requirementData.masterGemstoneId!==null&& !completedSteps[currentStep-1])? true : false}
            />
          </div>
          <div>
            <div id="MasterGemstone">
              <div className="mb-3 px-3">
                <div className="flex justify-between items-center">
                <h4 className="text-lg">Material</h4>
                <Tooltip title="Reset material" onClick={handleReset}>
                  <IconButton>
                  <RestartAltIcon />
                  </IconButton>
                </Tooltip>
                </div>
                <div className="grid grid-cols-5 gap-2 px-6 py-3 border ">
    {kindMasterGemstone.map((val, index) => {
        return (
            <label 
                key={val + index} 
                htmlFor={"material-" + index} 
                className="cursor-pointer"
            >
                <div className="">
                    <input 
                        type="radio" 
                        name="kind" 
                        id={"material-" + index} 
                        value={val} 
                        className="inline-block" 
                        data_object="MasterGemstone" 
                        checked={dataSelected.MasterGemstone!==null && dataSelected.MasterGemstone.kind==val}
                        onChange={HandleChangeData} 
                    />
                    <p className="ml-3  inline-block">{val}</p>
                </div>
            </label>
        );
    })}
</div>

             </div>
             <div className="grid grid-cols-2 gap-x-10 px-3">
              <div>
                <label htmlFor="size" className="text-lg">Size of Mastergemstone</label>
                <select 
                    label="Size of Mastergemstone"
                    data_object="MasterGemstone"
                    name="size"
                    key="mastergemstonesize"
                    onChange={HandleChangeData} 
                    value={dataSelected.MasterGemstone== null || dataSelected.MasterGemstone.size == null? "":dataSelected.MasterGemstone.size }
                    className="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500"
                >
                    <option key="defaultSelect" value="">--Choose MasterGemStone size--</option>
                    {sizeMasterGemstone.map((items, index) => (
                        <option key={items + index} value={items} >{items}</option>
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
                    value={dataSelected.MasterGemstone== null || dataSelected.MasterGemstone.shape == null? "":dataSelected.MasterGemstone.shape }
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
              defaultChecked={(requirementData.stonesId>0 || !completedSteps[currentStep-1]) && requirementData.stonesId!==null? true : false}
              
            />
          </div>
          <div id="Stones">
          <div className="mb-3 px-3">
                <div className="flex justify-between items-center">
                  <h4 className="text-lg">Material</h4>
                  <Tooltip title="Reset material" onClick={handleReset}>
                    <IconButton>
                    <RestartAltIcon />
                    </IconButton>
                  </Tooltip>
                </div>
                <div className="grid grid-cols-5 gap-2 px-6 py-3 border ">
    {kindStones.map((val, index) => {
        return (
            <label 
                key={val + index} 
                htmlFor={"material-" + val} 
                className="cursor-pointer"
            >
                <div className="">
                    <input 
                        type="radio" 
                        name="kindStones" 
                        id={"material-" + val} 
                        value={val} 
                        className="inline-block" 
                        data_object="Stones" 
                        checked={dataSelected.Stones!==null && dataSelected.Stones.kind==val}
                        onChange={HandleChangeData} 
                    />
                    <p className="ml-3  inline-block">{val}</p>
                </div>
            </label>
        );
    })}
  </div>
</div>
          <div className="grid grid-cols-2 gap-x-10 px-3">
              <div>
                <label htmlFor="size" className="text-lg">Size</label>
                <select 
                key="sizeOfStones"
                label="Size of Stones"
                data_object="Stones"
                name="size"
                onChange={HandleChangeData} 
                value={dataSelected.Stones== null || dataSelected.Stones.size == null? "":dataSelected.Stones.size }
                className="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500">
                  <option value="">--Choose Stones size--</option>
                  {sizeStones.map((item,index) => {
                    return (<option key={item} value={item}>{item}</option>)
                  })}
                </select>
              </div>
              <div>
                <label htmlFor="shape" className="text-lg">Quantity of Stones</label>
                <select 
                key="quantityStones"
                  data_object="Stones"
                  name="quantity"
                  onChange={HandleChangeData}
                  value={dataSelected.Stones== null || dataSelected.Stones.quantity == null? "":dataSelected.Stones.quantity }
                  className="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500" >
                  <option value="">--Choose Stones quantity--</option>
                  {quantityStones.map((item,index) => {
                    return (<option key={item} value={item}>{item}</option>)
                  })}
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
