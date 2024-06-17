import api from '../instance.jsx'
import axiosConfigHeader from '../AxiosConfigHeader.jsx'

const PostApiDesign = async ( formData) => {
    try {
        const response = await api.post(`/Design/DesignChild?parentId=${formData.parentId}`, formData, axiosConfigHeader);
            const designCreate = response.data;
            if (response.status === 200 || response.status === 201) {
                console.log("Created !!!");
            }else{
                console.log("Failed");
            }
            return designCreate;
        
    } catch (e) {
        console.error('Error during Post design:', e);
    }
}
export { PostApiDesign }