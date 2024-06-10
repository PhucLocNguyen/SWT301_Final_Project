import { useState, useEffect } from 'react'
import api from './instance.jsx'

const LoginApi = async (pathReq, formData, axiosConfig) => {

    try {
        const response = await api.post(`/User/${pathReq}`, formData, axiosConfig).then((res)=>
        {
            if(pathReq==="login" && res.status === 200) {localStorage.setItem("userInfo",JSON.stringify(res.data))}
            
        })
    } catch(e) {
        console.error('Error during login:', e);
    }
}
export { LoginApi }