import { useState, useEffect } from 'react'
import api from '../instance.jsx'

const FetchApiMasterGemstone = async () => {
   try {
      const response = await api.get('/MasterGemstone');
      return response.data; // Directly return the data from the response
   } catch (error) {
      console.error(error);
      return []; // Return an empty array or handle the error as needed
   }
}

export { FetchApiMasterGemstone }
