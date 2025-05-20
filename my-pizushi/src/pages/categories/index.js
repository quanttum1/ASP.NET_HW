import {useEffect, useState} from "react";
import {Link} from "react-router-dom";
import axiosInstance from "../../api/axiosInstance";
import {BASE_URL} from "../../api/apiConfig";

const CategoriesPage = () => {

    const [list, setList] = useState([])

    useEffect(() => {
        axiosInstance.get("/api/Categories")
            .then(res => {
                const {data} = res;
                console.log("Get list of Categories", data);
                setList(data);
            })
            .catch(err => console.log("Problem", err));
        console.log('UseEffect APP', "Викликаємо після рендера");
    },[]);

    return (
        <>
            <h1 className={"text-center"}>Категорії</h1>
            <Link to={"create"} className={"btn btn-primary"}>Додати</Link>
            {list.length === 0 ? <h2>Список пустий</h2> :
                <table className="table">
                    <thead>
                    <tr>
                        <th>#</th>
                        <th>Назва</th>
                        <th>Image</th>
                        <th>Edit</th>
                    </tr>
                    </thead>
                    <tbody>
                    {
                        list.map((item) => (
                            <tr key={item.id}>
                                <td>{item.id}</td>
                                <td>{item.name}</td>
                                <td><img src={`${BASE_URL}/images/200_${item.image}`} alt={item.name} width={75}/></td>
                                <td><Link to={`edit?id=${item.id}`}>Edit</Link></td>
                            </tr>
                        ))
                    }
                    </tbody>
                </table>
            }
        </>
    )
}

export default CategoriesPage;
