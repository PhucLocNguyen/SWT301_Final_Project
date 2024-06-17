import { useState, useEffect } from 'react'
import api from '../instance.jsx'

const FetchApiMasterGemstone = async (minSize, maxSize) => {
   try {
      const response = await api.get('/MasterGemstone');
      var dataMasterGemstones = response.data;
      const filteredData = dataMasterGemstones.filter((current) => {
       if(current.size >=minSize && current.size<maxSize){
         return true;
       }else{
         return false;
       }
      });

      return filteredData; // Directly return the data from the response
   } catch (error) {
      console.error(error);
      return []; // Return an empty array or handle the error as needed
   }
}
const FetchApiMasterGemstoneById = async (id) => {
   try {
      const response = await api.get(`/MasterGemstone/${id}`);
      return response.data; // Directly return the data from the response
   } catch (error) {
      console.error(error);
      return {}; // Return an empty array or handle the error as needed
   }
}
export { FetchApiMasterGemstone,FetchApiMasterGemstoneById }
