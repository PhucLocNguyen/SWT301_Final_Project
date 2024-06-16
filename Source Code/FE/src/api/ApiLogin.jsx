import { useState, useEffect } from 'react'
import api from './instance.jsx'
import { jwtDecode } from 'jwt-decode'

const LoginApi = async (pathReq, formData, axiosConfig) => {
    try {
        const response = await api.post(`/User/${pathReq}`, formData, axiosConfig).then((res) => {
            if (pathReq === "login" && res.status === 200) {
                localStorage.setItem("userInfo", JSON.stringify(res.data))
                console.log(jwtDecode(res.data).given_name)
                const accessToken = res.data
                const role = jwtDecode(res.data).given_name
                return { role, accessToken }
            }

        })
    } catch (e) {
        console.error('Error during login:', e);
    }
}
export { LoginApi }