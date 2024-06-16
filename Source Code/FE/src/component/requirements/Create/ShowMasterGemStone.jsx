import React from "react";
import { createRoot } from "react-dom/client";
function ShowMasterGemStone(props) {
    console.log(props);
  var getBoxMasterGemStone = document.getElementById(
    "MasterGemstoneContainerFloat"
  );
  if (getBoxMasterGemStone) {
    const root = createRoot(getBoxMasterGemStone);
    root.render(<ViewDetailMasterGemStone props={props} />);
  } else {
    console.error("Element with id 'MasterGemstoneContainerFloat' not found.");
  }
}

function ViewDetailMasterGemStone({ props }) {
  return (
    <>
    <div className="relative h-full w-full right-0 overflow-hidden ">

      <div className="flex justify-around p-3 border-b">
        <h4>Clarity: {props.clarity} </h4>
        <h4>Cut: {props.cut} </h4>
        <h4>Weight: {props.weight} </h4>
      </div>
      <div className="absolute -translate-y-1/2 -translate-x-1/2 w-full left-1/2 top-1/2 ">
        <img src={props.image} className="mx-auto w-full max-w-[95%] object-cover rounded-lg" alt={"image of "+props.kind+` ${props.shape}`} />
        <h4 className="text-center text-[20px]">{props.price}$</h4>
      </div>
      
    </div>
    </>
  );
}

export default ShowMasterGemStone;
