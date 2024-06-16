import { motion } from "framer-motion";
import { useCallback, useEffect } from "react";
import { useContext, useState } from "react";
import { fetchApiDesignById } from "../../../api/FetchApiDesign";
import { FetchApiMasterGemstoneById } from "../../../api/Requirements/FetchApiMasterGemstone";
import { FetchApiMaterialById } from "../../../api/Requirements/FetchApiMaterial";
import { FetchApiStonesById } from "../../../api/Requirements/FetchApiStones";
import { CustomButton } from "../../home/Home";
import { multiStepContext } from "./StepContext";

function ThirdStep({handleCompleteStep}) {
  const { currentStep, setCurrentStep, requirementData, setRequirementData, setIsSubmit } =
    useContext(multiStepContext);
  const [design, setDesign] = useState({});
  const [material, setMaterial] = useState(null);
  const [masterGemstone, setMasterGemstone] = useState(null);
  const [stones, setStones] = useState(null);
  const [data, setData] = useState({});
  const [typeOfJewellery, setTypeOfJewellery] = useState({});

  const SubmitRequirement = (e) => {
    e.preventDefault();
    setRequirementData({ ...requirementData, ...data });
    setIsSubmit(true);
    handleCompleteStep(currentStep-1);
  };

  useEffect(() => {
    const reachingData = async () => {
      const getDesignById = await fetchApiDesignById(requirementData.designParentId);
      setDesign({ ...getDesignById });
      setTypeOfJewellery(getDesignById.typeOfJewellery);

      if (requirementData.masterGemstoneId !== null && requirementData.masterGemstoneId !== 0) {
        const getMasterGemstone = await FetchApiMasterGemstoneById(requirementData.masterGemstoneId);
        setMasterGemstone({ ...getMasterGemstone });
      }

      if (requirementData.stonesId !== null && requirementData.stonesId !== 0) {
        const getStones = await FetchApiStonesById(requirementData.stonesId);
        setStones({ ...getStones });
      }

      const getMaterial = await FetchApiMaterialById(requirementData.material);
      setMaterial({ ...getMaterial });
    };

    reachingData();
  }, [requirementData]);

  // technique input value data
  const HandleChangeData = (e) => {
    const { name, value } = e.target;
    setData((prevData) => ({
      ...prevData,
      [name]: value,
    }));
  };

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

  const debouncedOnChange = useCallback(debounce(HandleChangeData, 1000), []);

  return (
    <>
      <motion.div initial={{ opacity: 0, x: 50 }} whileInView={{ opacity: 1, x: 0 }} className="mx-16">
        <div className="mt-3">
          <h2 className="text-[24px] mb-3 mt-3">Your requirement including:</h2>
          <div className="border-b pb-3 px-3">
            <div className="grid grid-cols-2">
              <img src={design.image} className="w-full h-[200px] object-cover rounded-md" alt="" />
              <div className="px-3">
                <h2 className="text-[20px] mb-1">Design: {design.designName}</h2>
                <ul className="ml-6 px-3 list-disc">
                  <li>Size for jewelry: {requirementData.size}</li>
                  <li>Material for jewelry: {material?.name}</li>
                  <li>Type of jewelry: {typeOfJewellery?.name}</li>
                </ul>
              </div>
            </div>
          </div>
          {masterGemstone && (
            <div className="border-b pb-3 grid pt-3 grid-cols-2 px-3">
              <div>
                <h3 className="text-[20px] mb-1 mt-3">MasterGemstone:</h3>
                <ul className="ml-3 px-3 list-disc">
                  <li>Kind: {masterGemstone.kind}</li>
                  <li>Size: {masterGemstone.size}</li>
                  <li>Shape: {masterGemstone.shape}</li>
                </ul>
              </div>
              <img src={masterGemstone.image} className="w-full h-[200px] object-cover rounded-md" alt="" />
            </div>
          )}
          {stones && (
            <div className="pb-3 px-3 border-b">
              <h3 className="text-[20px] mb-1 mt-3">Stones:</h3>
              <ul className="px-3">
                <li>Kind: {stones.kind}</li>
                <li>Size: {stones.size}</li>
                <li>Quantity: {stones.quantity}</li>
              </ul>
            </div>
          )}
          <div className="pb-3 px-3 mt-3">
            <label htmlFor="message" className="text-[20px] mb-1 mt-3">Notes:</label>
            <textarea
              id="message"
              name="customerNote"
              onChange={debouncedOnChange}
              rows="4"
              className="block p-2.5 w-full text-sm text-gray-900 bg-gray-50 rounded-lg border border-gray-300 focus:ring-blue-500 focus:border-blue-500 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500"
              placeholder="Write your thoughts here..."
            ></textarea>
          </div>
        </div>
        <CustomButton
          variant="contained"
          sx={{
            color: "#fff",
            bgcolor: "#000",
            letterSpacing: 4,
            padding: "0.7rem 2.375rem",
            fontSize: "1rem",
            fontWeight: 400,
            lineHeight: "1.5rem",
            display: "flex",
            justifyContent: "center",
            width: "100%",
          }}
          onClick={SubmitRequirement}
        >
          Submit
        </CustomButton>
      </motion.div>
    </>
  );
}

export default ThirdStep;
