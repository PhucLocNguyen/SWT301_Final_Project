import { useState, useEffect } from 'react'
import api from '../instance.jsx'

const FetchApiStones = async () => {
   try {
      const response = await api.get('/Stones');
      return response.data; // Directly return the data from the response
   } catch (error) {
      console.error(error);
      return []; // Return an empty array or handle the error as needed
   }
}
const FetchApiStonesById = async (id) => {
   try {
      const response = await api.get(`/Stones/${id}`);
      return response.data; // Directly return the data from the response
   } catch (error) {
      console.error(error);
      return {}; // Return an empty array or handle the error as needed
   }
}
export { FetchApiStones, FetchApiStonesById }
