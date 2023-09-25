export interface User{
    email: string;
    id?: string;
    role:string;
}

export interface UserToken{
    preferred_username: string;
    oid: string;
    name?:string;
}
export interface MyToken{
    $id?:string,
    role:string,
    id:string
}

export interface UserResponse{
    id$?:string,
    $values: User[]

}
