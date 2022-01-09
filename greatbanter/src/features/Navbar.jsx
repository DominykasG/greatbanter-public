import React, {useEffect} from 'react'
import { Link } from 'react-router-dom';
import {BiTrophy, BiDice6, BiPlus} from 'react-icons/bi';
import { useSelector, useDispatch } from 'react-redux'
import { logout, getUser } from '../reducers/user';

export default function Navbar() {
    const dispatch = useDispatch();
    const user = useSelector(state => state.user.loggedInUser)

    useEffect(() => {
        dispatch(getUser())
    }, [dispatch]);

    function onLogOutClick() {
        dispatch(logout())
    }
    return (
        <div className="navbar navbar-dark bg-dark">
            <div className="container-fluid">
                <div className="d-flex justify-content-between w-100">
                    <div className="text-light d-flex align-items-center justify-content-start">
                        <div className="font-weight-bold"><h3>GREAT BANTER REMASTERED</h3></div>
                    </div>
                    <div className="d-flex flex-grow-1 align-items-center justify-content-center icon-size">
                        <Link to="/random" className="text-light d-flex align-items-center"><BiDice6></BiDice6></Link> 
                    </div>
                    <div className="d-flex flex-grow-1 align-items-center justify-content-center icon-size">
                        <Link to="/greatest" className="text-light d-flex align-items-center"><BiTrophy></BiTrophy></Link>
                    </div>
                    {
                        !user ? (
                            <div className="d-flex align-items-center justify-content-end">
                            <Link to="/login" className="text-light mr-2">login</Link>
                            <Link to="/register" className="text-light ml-2">register</Link>
                        </div>) : (
                            <>
                                <div className="d-flex flex-grow-1 align-items-center justify-content-center icon-size">
                                    <Link to="/add" className="text-light d-flex align-items-center"><BiPlus className="icon-size"></BiPlus></Link>
                                </div>
                                <div className='d-flex justify-content-end'>
                                    <div className="d-flex align-items-center">
                                        <div className="text-light mr-2">{user.username}</div>
                                    </div>
                                    <div className="d-flex align-items-center">
                                        <button onClick={() => onLogOutClick()} className="btn btn-danger ml-2 rounded-0 shadow-none">logout</button>
                                    </div>
                                </div>
                            </>
                        )
                    }
                </div>
            </div>
        </div>
    )
}
