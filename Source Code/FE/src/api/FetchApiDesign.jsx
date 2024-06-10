import { useState, useEffect } from 'react'
import api from './instance.jsx'

const fetchApiDesign = async () => {

   try {

      let design = await api.get('/design');

      console.log("Fetch API Design log")

      return design.data
   } catch (error) {
      console.log(error)
   }

}

export { fetchApiDesign }