import React, { useEffect, useRef } from 'react'
import type { MouseEventHandler } from 'react'
import type { CoffeeItem } from '../../types'
import AdminForm from '../admin/AdminForm'
import useAdminModal from './use-adminModal'
import useCoffeesModal from './use-coffeesModal'
import './modalWindow.scss'

interface ModalWindowProps {
	onClose: MouseEventHandler<HTMLDivElement>;
	title: string;
	isVisible: boolean;
	coffee?: CoffeeItem | null;
}

const ModalWindow = ({ onClose, title, isVisible, coffee }: ModalWindowProps) => {
	const modalRef = useRef<HTMLDivElement | null>(null)
	const [adminIsOpen, , adminCloseModalWindow] = useAdminModal()
	const [coffeeIsOpen, , coffeeCloseModalWindow] = useCoffeesModal()
	
	const closeModal = () => {
		adminCloseModalWindow()
		coffeeCloseModalWindow()
	}
	
	const handleKeyDown = (event: React.KeyboardEvent<HTMLDivElement>) => {
		if (event.key === 'Escape') {
			closeModal()
		}
	}
	
	useEffect(() => {
		if (modalRef.current) {
			modalRef.current.focus()
		}
	}, [adminIsOpen, coffeeIsOpen])
	
	return (
		<div
			className={`modal ${isVisible ? 'show' : 'hide'}`}
			tabIndex={-1}
			onKeyDown={handleKeyDown}
			ref={modalRef}
		>
			<div className='modal__dialog'>
				<div className='modal__content'>
					<div className='modal__close' onClick={onClose}>&times;</div>
					<AdminForm title={title} coffee={coffee} />
				</div>
			</div>
		</div>
	)
}

export default ModalWindow
