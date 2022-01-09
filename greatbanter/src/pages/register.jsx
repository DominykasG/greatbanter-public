import React, {useState} from 'react'
import {useDispatch} from 'react-redux'
import { register } from '../reducers/user';

export default function Register() {
    const dispatch = useDispatch()
    const [registerData, setRegisterData] = useState({
        username:"",
        password:"",
        confirmedPassword:""
    });
    function onRegisterDataChange(e) {
        const { name, value } = e.target
        setRegisterData({...registerData, [name]:value })
    }
    function onRegisterClick() {
        if((registerData.username !== null && registerData.password !== null) && (registerData.confirmedPassword === registerData.password)) {
            dispatch(register({username: registerData.username, password : registerData.password}))
        }
    }
    return (
        <div className="container mt-5">
            <div className="row justify-content-center">
                <div className="col-4 form-background">
                    <label className="col-12 mt-2 text-light text-center font-weight-bold">REGISTER</label>
                    <div className="col-12 form-group">
                        <label htmlFor="inputUsername text-light">username</label>
                        <input onChange={(e) => onRegisterDataChange(e)} type="username" className="form-control shadow-none rounded-0 border-0" id="inputUsername" name="username"></input>
                    </div>
                    <div className="col-12  form-group">
                        <label htmlFor="inputPassword">password</label>
                        <input onChange={(e) => onRegisterDataChange(e)} type="password" className="form-control shadow-none rounded-0 border-0" id="inputPassword" name="password"></input>
                    </div>
                    <div className="col-12  form-group">
                        <input onChange={(e) => onRegisterDataChange(e)} type="password" className="form-control shadow-none rounded-0 border-0" id="confirmedPassword" name="confirmedPassword"></input>
                    </div>
                    <div className="d-flex justify-content-end">
                        <button onClick={()=> onRegisterClick()} className="btn text-right mr-3 login-btn text-light mb-3 font-weight-bold">SUBMIT</button>
                    </div>   
                </div>     
            </div>
        </div>
    )
}
