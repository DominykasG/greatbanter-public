import React, { useState, useEffect, useCallback} from 'react'
import Banter from '../components/Banter';
export default function Random() {
    const [banter, setBanter] = useState({})

    useEffect(() => {
        FetchAll()
    }, [])

    function FetchAll(){
        fetch(`https://localhost:44310/api/banters/GetRandom`)
        .then(response => response.json())
        .then(json => setBanter(json))
    }

    return (
        <Banter body={banter.content} name={banter.user?.username} score={banter.score} getRandom={FetchAll} button="RANDOM"  banterId={banter.id}></Banter>
    )
}
