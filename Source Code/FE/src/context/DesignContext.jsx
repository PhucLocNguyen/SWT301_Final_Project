import { createContext, useState } from "react";
import { fetchApiDesign } from '../api/FetchApiDesign.jsx'
import { useEffect } from "react";

const DesignContext = createContext()

const DesignProvider = ({ children }) => {
   const [design, setDesign] = useState(null)

   useEffect(() => {
      const fetchData = async () => {
         try {
            const { design } = await fetchApiDesign()
            setDesign(design)
         } catch (error) {
            console.log("Design Provider Error: ", error)
         }
      }
      fetchData()
   })

   return (
      <DesignContext.Provider value={{ design }}>
         {children}
      </DesignContext.Provider>
   )
}

export { DesignContext, DesignProvider }