import React, { useEffect } from 'react'
import { useDispatch, useSelector } from 'react-redux'
import { getUser } from '../reducers/user';

export default function AuthProvider(props) {
    const dispatch = useDispatch();
    const isLoggedIn = useSelector((state) => state.user.isLoggedIn);
    
    useEffect(() => {
        if(isLoggedIn){
            dispatch(getUser())
        }
    }, [isLoggedIn, dispatch])

    return (
        <>
            {props.children}
        </>
    )
}
