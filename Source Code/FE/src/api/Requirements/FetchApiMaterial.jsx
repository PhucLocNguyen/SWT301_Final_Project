import api from '../instance.jsx'

const FetchApiMaterial = async () => {
   try {
      const response = await api.get('/Material');
      return response.data; // Directly return the data from the response
   } catch (error) {
      console.error(error);
      return []; // Return an empty array or handle the error as needed
   }
}
const FetchApiMaterialById = async (id) => {
   try {
      const response = await api.get(`/Material/${id}`);
      return response.data; // Directly return the data from the response
   } catch (error) {
      console.error(error);
      return []; // Return an empty array or handle the error as needed
   }
}

export { FetchApiMaterial,FetchApiMaterialById }
