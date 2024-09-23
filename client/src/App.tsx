import { useEffect, useRef } from 'react'
import { BrowserRouter as Router, Route, Routes, useLocation } from 'react-router-dom'
import { CSSTransition, SwitchTransition } from 'react-transition-group'
import './styles/app.scss'
import { setUser, setUserAuthenticated } from './features/auth/auth-slice.ts'
import ConfirmEmail from './features/auth/ConfirmEmail.tsx'
import PrivateRoute from './features/auth/PrivateRoute.tsx'
import { AdminPanel, ControlsOurCoffee, AdminPanelOurPleasure, ControlsOurPleasure, Main, NotFound, OurCoffee, Pleasure } from './pages'
import { useAppDispatch } from './store/store.ts'

const AnimatedRoutes = () => {
	const dispatch = useAppDispatch()
	const location = useLocation()
	const nodeRef = useRef(null)
	const duration = 300
	
	const getLocalStorageItem = (key: string) => {
		try {
			const item = localStorage.getItem(key)
			return item ? JSON.parse(item) : null
		} catch (error) {
			console.error(`Failed to parse ${key} from localStorage`, error)
			return null
		}
	}
	
	const user = getLocalStorageItem('user')
	
	useEffect(() => {
		if (user && user.jwtToken) {
			dispatch(setUser(user))
			dispatch(setUserAuthenticated())
		}
	}, [dispatch, user])
	
	return (
		<SwitchTransition>
			<CSSTransition
				key={location.pathname}
				nodeRef={nodeRef}
				timeout={duration}
				classNames='my-node'
				unmountOnExit
			>
				<div ref={nodeRef}>
					<Routes location={location}>
						<Route path='/' element={<Main />} />
						<Route path='/OurCoffee' element={<PrivateRoute><OurCoffee /></PrivateRoute>} />
						<Route path='/OurCoffee/:id' element={<PrivateRoute><ControlsOurCoffee /></PrivateRoute>} />
						<Route path='/Pleasure' element={<PrivateRoute><Pleasure /></PrivateRoute>} />
						<Route path='/Pleasure/:id' element={<PrivateRoute><ControlsOurPleasure /></PrivateRoute>} />
						<Route path='/AdminPanel' element={<PrivateRoute restricted={true}><AdminPanel /></PrivateRoute>} />
						<Route path='/AdminPanel/:id' element={<PrivateRoute><AdminPanelOurPleasure /></PrivateRoute>} />
						<Route path='/ConfirmEmail' element={<PrivateRoute><ConfirmEmail /></PrivateRoute>} />
						<Route path='*' element={<NotFound />} />
					</Routes>
				</div>
			</CSSTransition>
		</SwitchTransition>
	)
}

function App() {
	return (
		<Router>
			<AnimatedRoutes />
		</Router>
	)
}

export default App
