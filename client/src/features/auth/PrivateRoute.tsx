import { ReactNode } from 'react'
import { useSelector } from 'react-redux'
import { selectAuthUser } from './auth-Selectors.ts'
import LoadingToRedirect from './LoadingToRedirect.tsx'

type PrivateRouteProps = {
	children: ReactNode
	restricted?: boolean
}

const PrivateRoute = ({ children, restricted = false }: PrivateRouteProps) => {
	const { jwtToken, role } = useSelector(selectAuthUser)
	
	if (!jwtToken) {
		return <LoadingToRedirect message='You need to log in to access this page.' />
	}
	
	if (restricted && role !== 'Administrator') {
		return <LoadingToRedirect message='You do not have permission to access this page.' />
	}
	
	return children
}

export default PrivateRoute
