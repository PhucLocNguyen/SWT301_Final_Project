import { Button, SvgIcon, Icon } from '@mui/material';
import { jwtDecode } from 'jwt-decode';
import InputPassword from './InputPassword';
import InputText from './InputText';
import { useCallback, useEffect, useRef, useState } from 'react';
import { motion } from "framer-motion";
import axios from 'axios';
import { LoginApi } from '../../api/ApiLogin';
import useAuth from '../../hooks/useAuth.jsx'


function Login() {
    const { setAuth } = useAuth();

    let [isToggle, setIsToggle] = useState(false);
    const [dataSource, setDataSource] = useState([]);
    
    const [formData, setFormData] = useState({
        username: "", password:"",
    });
    const axiosConfig = {
        headers: {
            'Content-Type': 'application/json',
        },
        withCredentials: true // Nếu API của bạn yêu cầu cookie
    };
    useEffect(()=>{ 
        if(!isToggle){
            setFormData({ username: "", password:""});
        }else{
            setFormData({ username: "", email:"" ,password:"",});
        }
    },[isToggle]);
    console.log(formData); 
    const HandleSubmit = async (e)=>{
        e.preventDefault();
        let pathReq = "register";
        //reset cac field trong form
        const form = e.target;
        var data = new FormData(form);
        const listState ={};
        await Object.entries(formData).forEach(([key, value]) => {
            listState[key] =data.get(key);
        });
        await setFormData(listState);
        var htmlCollection =[...e.target];
        htmlCollection.forEach((element, index)=>{
            if(element.tagName === "INPUT"){
                element.value = "";
                element.blur();
            }
        })
        if(e.target.name==="login"){
            pathReq="login";
        }
       var loginApi = LoginApi(pathReq,listState, axiosConfig);
       const { role , accessToken } = loginApi
        setAuth( {role, accessToken} )
    }
   
   
    return (
        <div className="bg-[#c9d6ff] w-full h-screen bg-gradient-to-r from-purple-500 to-pink-500 flex items-center justify-center flex-col">
            <div className="bg-[#fff] rounded-[30px] shadow-[0_5px_15px_rgba(0,0,0,0.35)] relative overflow-hidden w-[768px] max-w-[100%] min-h-[480px]">

                <motion.div
                    animate={{
                        x: isToggle ? '100%' : 0,
                        opacity: isToggle ? [0, 1] : 0,
                        zIndex: isToggle ? 3 : 1
                    }}
                    transition={{
                        duration: 0.6,
                        ease: 'linear'
                    }}
                    className='absolute h-[100%] top-0 left-0 w-[50%] z-1 opacity-0'
                >
                    <form method='POST' onSubmit={(e)=>HandleSubmit(e)} className='bg-[#fff] flex items-center justify-center flex-col h-[100%] px-[40px]' name='register'>
                        <h1 className='font-bold text-[35px]'>Create Account</h1>
                        <div className='my-[10px]'>
                            <motion.a whileHover={{ scale: 1.2 }} href='#' className='border-[2px] border-solid border-[#ccc] rounded-[20%] inline-flex justify-center items-center mx-[4px] w-[40px] h-[40px]'>
                                <SvgIcon>
                                    <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 640 512">
                                        <path d="M386.1 228.5c1.8 9.7 3.1 19.4 3.1 32C389.2 370.2 315.6 448 204.8 448c-106.1 0-192-85.9-192-192s85.9-192 192-192c51.9 0 95.1 18.9 128.6 50.3l-52.1 50c-14.1-13.6-39-29.6-76.5-29.6-65.5 0-118.9 54.2-118.9 121.3 0 67.1 53.4 121.3 118.9 121.3 76 0 104.5-54.7 109-82.8H204.8v-66h181.3zm185.4 6.4V179.2h-56v55.7h-55.7v56h55.7v55.7h56v-55.7H627.2v-56h-55.7z" />
                                    </svg>
                                </SvgIcon>
                            </motion.a>
                        </div>
                        <span className="text-[13px]">or use your email for registeration</span>

                        <InputText label='username' type='text'></InputText>
                        <InputText label='email' type='email' pattern="^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"></InputText>
                        <InputPassword label='password' inputCase='register'></InputPassword>
                        <Button variant="contained" type='submit'>Sign Up</Button>
                    </form>
                </motion.div>

                {/* ------------------------------------------------------------ */}

                <motion.div
                    animate={{
                        x: isToggle ? '100%' : 0,
                        zIndex: isToggle ? 2 : 3
                    }}
                    transition={{
                        duration: 0.6,
                        ease: 'linear'
                    }}
                    className='absolute h-[100%] top-0 left-0 w-[50%] z-2'
                >
                    <form method='POST' onSubmit={HandleSubmit} className='bg-[#fff] flex items-center justify-center flex-col h-[100%] px-[40px]' name='login'>
                        <h1 className='font-bold text-[35px]'>Sign In</h1>
                        <div className='my-[10px]'>
                            <motion.a whileHover={{ scale: 1.2 }} href='#' className='border-[2px] border-solid border-[#ccc] rounded-[20%] inline-flex justify-center items-center mx-[4px] w-[40px] h-[40px]'>
                                <SvgIcon>
                                    <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 640 512">
                                        <path d="M386.1 228.5c1.8 9.7 3.1 19.4 3.1 32C389.2 370.2 315.6 448 204.8 448c-106.1 0-192-85.9-192-192s85.9-192 192-192c51.9 0 95.1 18.9 128.6 50.3l-52.1 50c-14.1-13.6-39-29.6-76.5-29.6-65.5 0-118.9 54.2-118.9 121.3 0 67.1 53.4 121.3 118.9 121.3 76 0 104.5-54.7 109-82.8H204.8v-66h181.3zm185.4 6.4V179.2h-56v55.7h-55.7v56h55.7v55.7h56v-55.7H627.2v-56h-55.7z" />
                                    </svg>
                                </SvgIcon>
                            </motion.a>
                        </div>
                        <span className="text-[13px]">or use your username password</span>

                        {/* <InputText label='Username' type='text' handleChild={debouncedOnChange} ></InputText>
                        <InputPassword label='Password' handleChild={debouncedOnChange}></InputPassword> */}
                        <InputText label='username' type='text' ></InputText>
                        <InputPassword label='password' ></InputPassword>
                        <a className="text-[#333] text-[13px] mt-[15px] mb-[10px]" href="#">Forget Your Password?</a>
                        <Button variant="contained" type='submit'>Sign In</Button>
                    </form>
                </motion.div>
                {/* ------------------------------------------------------------ */}

                <motion.div
                    animate={{
                        x: isToggle ? '-100%' : 0,
                        borderRadius: isToggle ? ['0 150px 100px 0'] : ['150px 0 0 100px'],
                        zIndex: isToggle ? 1000 : 1000
                    }}
                    transition={{
                        duration: 0.6,
                        ease: 'linear'
                    }}
                    className='absolute top-0 left-[50%] w-[50%] h-[100%] overflow-hidden rounded-tl-[150px] rounded-bl-[100px] z-1000'>
                    <motion.div
                        animate={{ x: isToggle ? '50%' : 0 }}
                        transition={{ duration: 0.6, ease: 'linear' }}
                        className='bg-[#512da8] h-[100%] text-[#fff] relative left-[-100%] w-[200%] translate-x-0 bg-gradient-to-r from-[#FFE0B5] to-[#D8AE7E]'
                    >
                        <motion.div
                            animate={{ x: isToggle ? 0 : '-200%' }}
                            transition={{ duration: 0.6, ease: 'linear' }}
                            className='absolute w-[50%] h-[100%] flex items-center justify-center flex-col px-[30px] text-center top-0 translate-x-0 translate-x-[-200%]'
                        >
                            <h1 className='font-bold text-[35px]'>Welcome Back!</h1>
                            <p className="text-[15px] leading-[20px] tracking-[0.3px] my-[20px]">
                                Enter your personal details to use all of site features
                            </p>
                            <Button onClick={() => { setIsToggle(!isToggle) }} className='bg-transparent border-[#fff]' variant="contained">Sign In</Button>
                        </motion.div>

                        <motion.div
                            animate={{ x: isToggle ? '200%' : 0 }}
                            transition={{ duration: 0.6, ease: 'linear' }}
                            className='absolute w-[50%] h-[100%] flex items-center justify-center flex-col px-[30px] text-center top-0 translate-x-0 right-0 '
                        >
                            <h1 className='font-bold text-[35px]'>Hello, Friend!</h1>
                            <p className="text-[15px] leading-[20px] tracking-[0.3px] my-[20px]">
                                Register with your personal details to use all of site features
                            </p>
                            <Button onClick={() => { setIsToggle(!isToggle) }} className='bg-transparent border-[#fff]' variant="contained">Sign Up</Button>
                        </motion.div>
                    </motion.div>
                </motion.div>
            </div>
        </div>
    )
}

export default Login