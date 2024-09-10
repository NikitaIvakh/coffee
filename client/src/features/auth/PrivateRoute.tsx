import { ReactNode } from 'react'
import { useSelector } from 'react-redux'
import { selectAuthUser } from './auth-Selectors.ts'
import LoadingToRedirect from './LoadingToRedirect.tsx'

const PrivateRoute = ({ children }: { children: ReactNode }) => {
	const { jwtToken } = useSelector(selectAuthUser)
	return jwtToken ? children : <LoadingToRedirect />
}

export default PrivateRoute