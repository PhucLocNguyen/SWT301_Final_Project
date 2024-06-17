import { motion } from 'framer-motion';

function Popup({ setIsOpenPopup }) {
  return (
    <div
      onClick={() => setIsOpenPopup(false)}
      className="fixed top-0 right-0 left-0 bottom-0 bg-[rgba(0,0,0,0.6)] flex items-center justify-center"
    >
      <motion.div
        initial={{ opacity: 0, scale: 0 }}
        animate={{ opacity: 1, scale: 1, transition: { duration: 0.5 } }}
        onClick={(e) => e.stopPropagation()}
        className="bg-[#fff] w-[40rem] rounded-[10px] min-h-[450px]"
      >
        <h1>Popup Content Here</h1>
      </motion.div>
    </div>
  );
}

export default Popup;
