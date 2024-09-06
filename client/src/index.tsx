import React, { StrictMode } from 'react'
import ReactDOM from 'react-dom/client'
import { Provider } from 'react-redux'
import App from './App'
import './styles/index.scss'
import './styles/style.scss'
import store from './store/store'
import './styles/mixin.scss'

const rootElement = document.getElementById('root') as HTMLDivElement
const root = ReactDOM.createRoot(rootElement)
root.render(
	<StrictMode>
		<Provider store={store}>
			<App />
		</Provider>
	</StrictMode>
)
