import React from 'react'
import {AiOutlineArrowDown, AiOutlineArrowUp} from 'react-icons/ai'
import axios from 'axios'

export default function Banter(props) {
    function onClickAddScore(banterId)
    {
        axios.post('https://localhost:44310/api/banters/AddScore', {id: banterId}, {withCredentials: true})
        props.getRandom()
    }
    function onClickRemoveScore(banterId)
    {
        axios.post('https://localhost:44310/api/banters/RemoveScore', {id: banterId}, {withCredentials: true})
        props.getRandom()
    }
    return (
            <div className="container h-100">
                <div className="row h-100 d-flex justify-content-center text-center flex-column align-items-center">
                    <div className="col-12 mt-5 nopadding d-flex justify-content-center flex-column">
                        <h1 className="text-light">{props.body}</h1>
                        <h4 className="text-light">{props.name}</h4>
                        <div>
                            <button className="btn" onClick={() => onClickAddScore(props.banterId)}>
                                <AiOutlineArrowUp className="text-primary"></AiOutlineArrowUp>
                            </button>
                            <div className="text-light">{props.score}</div>
                            <button className="btn" onClick={() => onClickRemoveScore(props.banterId)}>
                                <AiOutlineArrowDown className="text-danger"></AiOutlineArrowDown>
                            </button>   
                        </div>
                        <button className="btn text-light font-weight-bold" onClick={props.getRandom}>{props.button}</button>
                    </div>
                </div>
            </div>
    )
}
