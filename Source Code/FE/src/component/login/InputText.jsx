import { TextField } from '@mui/material'
import { useCallback, useState } from 'react';
import { useRef } from 'react';

function InputText({ label, type, pattern}) {
    const [useValid, setValid] = useState({isValid: false,
    message: ""});
    function ValidateInputChange(e){
        const value = e.target.value.trim();
        console.log(value.length);
        if(value.length ==0){
            setValid({isValid:true, message: `${label} field can't be empty!`});
            return;
        }
        if(!e.target.value.match(pattern)){
            setValid({isValid:true, message: `The input must match the pattern of ${label}`});
            return;
        }
        setValid({isValid: false});
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
        <TextField sx={{ my: '8px', width: '100%' }} type={type} id="outlined-basic" label={label} error={useValid.isValid} helperText={useValid.message} variant="outlined" name={label} onChange={debouncedOnChange} />
    )
}

export default InputText