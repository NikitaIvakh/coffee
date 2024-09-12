import { ReactNode } from 'react'
import { useSelector } from 'react-redux'
import { selectAuthUser } from './auth-Selectors.ts'
import LoadingToRedirect from './LoadingToRedirect.tsx'

type PrivateRouteProps = {
	children: ReactNode
	restricted?: boolean
}

const PrivateRoute = ({ children, restricted = false }: PrivateRouteProps) => {
	const user = useSelector(selectAuthUser)
	
	if (!user?.jwtToken) {
		return <LoadingToRedirect message='You need to log in to access this page.' />
	}
	
	if (restricted && user?.role !== 'Administrator') {
		return <LoadingToRedirect message='You do not have permission to access this page.' />
	}
	
	return children
}

export default PrivateRoute
