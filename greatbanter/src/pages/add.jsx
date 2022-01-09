import axios from 'axios'
import React, { useState, useEffect } from 'react'

export default function Add() {    
    const [charCount, setCharCount] = useState(0)
    const [radioValue, setRadioValue] = useState("false")
    const [message, setMessage] = useState({
        content:"",
        displayUsername: "true"
    })

    function handleChange(input){
        setMessage({...message, content: input.target.value})
        setCharCount( 0 + input.target.value.length)
    }
    function handleRadioChange(e)
    {
        if(e.target.value === "true")
        {
            setRadioValue("false")
        }
        else
        {
            setRadioValue("true")
        }
        setMessage({...message, displayUsername: e.target.value})
    }
    async function onMessageSubmit(){
        if(message.content)
        {
            await axios.post('https://localhost:44310/api/banters/AddBanter', message, {withCredentials: true, headers: {'Content-Type': 'application/json'}})
        }
    }
    return (
        <div className="container mt-5">
            <div className="row justify-content-center">
                <div className="col-8 col-md-4 form-background">
                    <div className="col-12 mt-2 text-light text-center font-weight-bold mb-2">ADD NEW</div>
                    <div className="col-12 form-group">
                        <textarea className="form-control shadow-none rounded-0" placeholder="type here" value={message.content} id="inputBanter" onChange={(e) => handleChange(e)}></textarea>
                    </div>
                    <div className="text-right mr-3 text-light mb-3">Characters Left: {charCount} / 255</div>
                    <div className="col-12 form-group">
                        <div className="form-check-inline">
                            <input onClick={(e) => handleRadioChange(e)} type="checkbox" className="form-check-input" id="showUsername" value={radioValue} name="showUsername"></input>
                            <label className="form-check-label text-light" htmlFor="showUsername">Don't show username</label>
                        </div>
                        <button  onClick={onMessageSubmit} className="btn p-0 float-right mr-3 login-btn text-light mb-3 shadow-none border-0" disabled={charCount>255?true:false}>submit</button>
                    </div>
                </div>     
            </div>
        </div>
    )
}
