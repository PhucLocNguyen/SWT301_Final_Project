import Navbar from "../nav/Navbar";
import Footer from "../footer/Footer";

function DefaultLayout({ children }) {
   return (
      <>
         <Navbar />
         <>
            {children}
         </>
         <Footer />
      </>
   )
}

export default DefaultLayout