import { useCallback, useState } from 'react';
import { Visibility, VisibilityOff } from '@mui/icons-material';
import { FormControl, OutlinedInput, InputAdornment, IconButton, InputLabel, TextField } from '@mui/material'
import { useEffect } from 'react';

function InputPassword({label, pattern, refInput, inputCase}) {
    const [showPassword, setShowPassword] = useState(false);

    const handleClickShowPassword = () => setShowPassword((show) => !show);

    const handleMouseDownPassword = (event) => {
        event.preventDefault();
    };
    const [useValid, setValid] = useState({isValid: false,
        message: ""});
        function ValidateInputChange(e){
            const value = e.target.value.trim();
            console.log(value.length);
            if (value.length === 0) {
                setValid({ isValid: true, message: `${label} field can't be empty!` });
                return;
            }
            console.log(inputCase);
            if(inputCase =="register"){
                // Kiểm tra mật khẩu có ít nhất một chữ cái viết hoa
                if (!/[A-Z]/.test(value)) {
                    setValid({ isValid: true, message: `${label} must contain at least one uppercase letter!` });
                    return;
                }
                
                // Kiểm tra mật khẩu có ít nhất một chữ cái viết thường
                if (!/[a-z]/.test(value)) {
                    setValid({ isValid: true, message: `${label} must contain at least one lowercase letter!` });
                    return;
                }
                
                // Kiểm tra mật khẩu có ít nhất một chữ số
                if (!/\d/.test(value)) {
                    setValid({ isValid: true, message: `${label} must contain at least one number!` });
                    return;
                }
                
                // Kiểm tra mật khẩu có ít nhất một ký tự đặc biệt trong danh sách
                if (!/[!@#$%^&*()]/.test(value)) {
                    setValid({ isValid: true, message: `${label} must contain at least one special character!` });
                    return;
                }
                
                // Kiểm tra mật khẩu có độ dài ít nhất là 12 ký tự
                if (value.length < 12) {
                    setValid({ isValid: true, message: `${label} must be at least 12 characters long!` });
                    return;
                }
            }
            setValid(false);
        }
        const debounce = (func, delay) => {
            let timeoutId;
            return (...args) => {
              if (timeoutId) {
                clearTimeout(timeoutId);
              }
              timeoutId = setTimeout(() => {
                func(...args);
              }, delay);
            };
          };
         const debouncedOnChange = useCallback(debounce(ValidateInputChange, 1000), [ValidateInputChange]);
       
    return (
        <FormControl sx={{ my: '8px', width: '100%' }} variant="outlined" error={useValid.isValid} >
            <InputLabel htmlFor="outlined-adornment-password">Password</InputLabel>
            <OutlinedInput
                id="outlined-adornment-password"
                type={showPassword ? 'text' : 'password'} 
                onChange={debouncedOnChange}
                name={label}
                
                endAdornment={
                    <InputAdornment position="start">
                        <IconButton
                            aria-label="toggle password visibility"
                            onClick={handleClickShowPassword}
                            onMouseDown={handleMouseDownPassword}
                            edge="end"
                        >   
                            {showPassword ? <VisibilityOff /> : <Visibility />}
                        </IconButton>
                    </InputAdornment>
                }
                
                label={label}
                
            />
           {useValid.isValid ? <p className=" text-sm tracking-tighter text-left mt-[3px] mr-[14px] ml-[14px] text-[#d32f2f]" >{useValid.message}</p> : null} 
        </FormControl>
    )
}

export default InputPassword